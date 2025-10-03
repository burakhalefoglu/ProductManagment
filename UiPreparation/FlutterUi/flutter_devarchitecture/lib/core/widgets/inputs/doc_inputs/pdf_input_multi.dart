import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:file_picker/file_picker.dart';
import '/core/widgets/inputs/doc_inputs/file_content_model.dart';

class CustomPdfInputMultiple extends StatefulWidget {
  final String labelText;
  final int minSizeKB;
  final int maxSizeKB;
  final int? minFileCount;
  final int? maxFileCount;
  final ValueChanged<List<FileContent>> onFilesSelected;
  final bool enabled;

  const CustomPdfInputMultiple({
    super.key,
    required this.labelText,
    required this.minSizeKB,
    required this.maxSizeKB,
    this.minFileCount,
    this.maxFileCount,
    required this.onFilesSelected,
    this.enabled = true,
  });

  @override
  State<CustomPdfInputMultiple> createState() => _CustomPdfInputMultipleState();
}

class _CustomPdfInputMultipleState extends State<CustomPdfInputMultiple> {
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
      type: FileType.custom,
      allowedExtensions: ['pdf'],
      allowMultiple: true,
    );

    setState(() {
      _isLoading = false;
    });

    if (result != null && result.files.isNotEmpty) {
      if (widget.maxFileCount != null &&
          result.files.length > widget.maxFileCount!) {
        setState(() {
          _errorText = "En fazla ${widget.maxFileCount} dosya seçebilirsiniz.";
          _selectedFiles = [];
        });
        widget.onFilesSelected([]);
        return;
      }

      List<FileContent> validFiles = [];

      for (var file in result.files) {
        if (!file.name.toLowerCase().endsWith('.pdf')) {
          _errorText = "Yalnızca PDF dosyalarına izin verilir.";
          continue;
        }

        int fileSizeKB = file.size ~/ 1024;
        if (fileSizeKB < widget.minSizeKB || fileSizeKB > widget.maxSizeKB) {
          _errorText =
              "${file.name} boyutu geçersiz (${widget.minSizeKB}-${widget.maxSizeKB}KB)";
          continue;
        }

        validFiles.add(FileContent(
          title: file.name,
          base64String: base64Encode(file.bytes!),
          type: 'pdf',
        ));
      }

      // Minimum kontrol
      if (widget.minFileCount != null &&
          validFiles.length < widget.minFileCount!) {
        setState(() {
          _errorText = "En az ${widget.minFileCount} dosya seçmelisiniz.";
          _selectedFiles = [];
        });
        widget.onFilesSelected([]);
        return;
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
                    ? 'PDF dosyaları seçin'
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
                        '- ${file.title}',
                        style: const TextStyle(
                            fontSize: 14, color: Colors.black87),
                      ))
                  .toList(),
            ),
          ),
      ],
    );
  }
}
