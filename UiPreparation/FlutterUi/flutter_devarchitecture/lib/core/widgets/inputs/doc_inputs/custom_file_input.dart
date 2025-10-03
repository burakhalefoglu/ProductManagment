import 'dart:convert';
import '/core/widgets/inputs/doc_inputs/file_content_model.dart';
import 'package:flutter/material.dart';
import 'package:file_picker/file_picker.dart';

class CustomFileInput extends StatefulWidget {
  final String labelText;
  final int minSizeKB;
  final int maxSizeKB;
  final ValueChanged<FileContent?> onFileSelected;
  final bool enabled;

  const CustomFileInput({
    super.key,
    required this.labelText,
    required this.minSizeKB,
    required this.maxSizeKB,
    required this.onFileSelected,
    this.enabled = true,
  });

  @override
  State<CustomFileInput> createState() => _CustomFileInputState();
}

class _CustomFileInputState extends State<CustomFileInput> {
  FileContent? _selectedFile;
  String? _errorText;
  bool _isLoading = false;

  // 🎯 Yaygın uzantılar tanımlandı:
  final List<String> _allowedExtensions = [
    'doc',
    'docx',
    'xls',
    'xlsx',
    'csv',
    'txt',
    'rtf',
    'md',
    'zip',
    'rar',
    '7z',
    'tar',
    'json',
    'xml',
    'html',
    'js'
  ];

  void _pickFile() async {
    if (!widget.enabled) return;

    setState(() {
      _isLoading = true;
      _errorText = null;
    });

    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: _allowedExtensions,
      withData: true,
    );

    setState(() {
      _isLoading = false;
    });

    if (result != null && result.files.isNotEmpty) {
      PlatformFile file = result.files.first;
      final ext = file.extension?.toLowerCase() ?? '';

      if (!_allowedExtensions.contains(ext)) {
        setState(() {
          _errorText =
              "Yalnızca şu dosya türlerine izin verilir: ${_allowedExtensions.join(', ')}";
          _selectedFile = null;
        });
        widget.onFileSelected(null);
        return;
      }

      final fileSizeKB = file.size ~/ 1024;

      if (fileSizeKB < widget.minSizeKB) {
        setState(() {
          _errorText = "${widget.labelText} min (${widget.minSizeKB}KB)";
          _selectedFile = null;
        });
        widget.onFileSelected(null);
      } else if (fileSizeKB > widget.maxSizeKB) {
        setState(() {
          _errorText = "${widget.labelText} max (${widget.maxSizeKB}KB)";
          _selectedFile = null;
        });
        widget.onFileSelected(null);
      } else {
        final content = FileContent(
            title: file.name,
            base64String: base64Encode(file.bytes!),
            type: ext);

        setState(() {
          _selectedFile = content;
          _errorText = null;
        });

        widget.onFileSelected(content);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        GestureDetector(
          onTap: _pickFile,
          child: AbsorbPointer(
            absorbing: true,
            child: TextFormField(
              enabled: widget.enabled,
              decoration: InputDecoration(
                labelText: widget.labelText,
                hintText: _selectedFile?.title ??
                    'Dosya seçin (${_allowedExtensions.join(', ')})',
                errorText: _errorText,
                suffixIcon: const Icon(Icons.attach_file),
              ),
            ),
          ),
        ),
        const SizedBox(height: 8),
        if (_isLoading) const LinearProgressIndicator(minHeight: 4),
        if (_selectedFile != null && !_isLoading)
          Padding(
            padding: const EdgeInsets.only(top: 8.0),
            child: Text(
              'Seçilen dosya: ${_selectedFile!.title}',
              style: const TextStyle(fontSize: 14, color: Colors.black87),
            ),
          ),
      ],
    );
  }
}
