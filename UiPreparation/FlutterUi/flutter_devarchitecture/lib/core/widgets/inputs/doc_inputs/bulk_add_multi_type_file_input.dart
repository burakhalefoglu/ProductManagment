import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:file_picker/file_picker.dart';
import 'file_content_model.dart';

class CustomMultiFileInput extends StatefulWidget {
  final String labelText;
  final int minSizeKB;
  final int maxSizeKB;
  final ValueChanged<List<FileContent>> onFilesSelected;
  final bool enabled;

  const CustomMultiFileInput({
    super.key,
    required this.labelText,
    required this.minSizeKB,
    required this.maxSizeKB,
    required this.onFilesSelected,
    this.enabled = true,
  });

  @override
  State<CustomMultiFileInput> createState() => _CustomMultiFileInputState();
}

class _CustomMultiFileInputState extends State<CustomMultiFileInput> {
  List<FileContent> _selectedFiles = [];
  String? _errorText;
  bool _isLoading = false;

  void _pickFiles() async {
    if (!widget.enabled) return;

    setState(() {
      _isLoading = true;
      _errorText = null;
    });

    FilePickerResult? result = await FilePicker.platform.pickFiles(
      allowMultiple: true,
      type: FileType.any,
      withData: true,
    );

    setState(() {
      _isLoading = false;
    });

    if (result != null && result.files.isNotEmpty) {
      List<FileContent> validFiles = [];

      for (var file in result.files) {
        final fileSizeKB = file.size ~/ 1024;

        if (file.bytes == null) continue;

        if (fileSizeKB < widget.minSizeKB || fileSizeKB > widget.maxSizeKB) {
          setState(() {
            _errorText =
                "Her dosya ${widget.minSizeKB}KB ile ${widget.maxSizeKB}KB arasında olmalı.";
          });
          widget.onFilesSelected([]);
          return;
        }
        validFiles.add(FileContent(
          title: file.name,
          base64String: base64Encode(file.bytes!),
          type: file.extension ?? 'bin',
        ));
      }

      setState(() {
        _selectedFiles = validFiles;
        _errorText = null;
      });

      widget.onFilesSelected(validFiles);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        GestureDetector(
          onTap: _pickFiles,
          child: AbsorbPointer(
            absorbing: true,
            child: TextFormField(
              enabled: widget.enabled,
              decoration: InputDecoration(
                labelText: widget.labelText,
                hintText: _selectedFiles.isEmpty
                    ? 'Dosya(lar) seçin'
                    : '${_selectedFiles.length} dosya seçildi',
                errorText: _errorText,
                suffixIcon: const Icon(Icons.attach_file),
              ),
            ),
          ),
        ),
        const SizedBox(height: 8),
        if (_isLoading) const LinearProgressIndicator(minHeight: 4),
        if (_selectedFiles.isNotEmpty && !_isLoading)
          Padding(
            padding: const EdgeInsets.only(top: 8.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: _selectedFiles
                  .map((file) => Text(
                        '• ${file.title}',
                        style: const TextStyle(fontSize: 14),
                      ))
                  .toList(),
            ),
          ),
      ],
    );
  }
}
