import 'dart:convert';
import 'dart:typed_data';
import '/core/widgets/inputs/doc_inputs/file_content_model.dart';
import 'package:flutter/material.dart';
import 'package:file_picker/file_picker.dart';

class CustomImageInput extends StatefulWidget {
  final String labelText;
  final int minSizeKB;
  final int maxSizeKB;
  final ValueChanged<FileContent?> onFileSelected;
  final bool enabled;

  const CustomImageInput({
    super.key,
    required this.labelText,
    required this.minSizeKB,
    required this.maxSizeKB,
    required this.onFileSelected,
    this.enabled = true,
  });

  @override
  State<CustomImageInput> createState() => _CustomImageInputState();
}

class _CustomImageInputState extends State<CustomImageInput> {
  FileContent? _selectedFile;
  String? _errorText;
  bool _isLoading = false;
  Uint8List? _previewBytes;

  void _pickFile() async {
    if (!widget.enabled) return;

    setState(() {
      _isLoading = true;
      _errorText = null;
      _previewBytes = null;
    });

    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['jpg', 'jpeg', 'png', 'webp'],
      withData: true,
    );

    setState(() {
      _isLoading = false;
    });

    if (result != null && result.files.isNotEmpty) {
      PlatformFile file = result.files.first;
      final ext = file.extension?.toLowerCase();

      if (!(ext == 'jpg' || ext == 'jpeg' || ext == 'png' || ext == 'webp')) {
        setState(() {
          _errorText =
              "Yalnızca görsel dosyalarına izin verilir (jpg, jpeg, png, webp).";
          _selectedFile = null;
        });
        widget.onFileSelected(null);
        return;
      }

      int fileSizeKB = file.size ~/ 1024;

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
          type: ext.toString(),
        );

        setState(() {
          _selectedFile = content;
          _errorText = null;
          _previewBytes = file.bytes!;
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
                hintText:
                    _selectedFile?.title ?? 'Görsel seçin (jpg, png, webp)',
                errorText: _errorText,
                suffixIcon: const Icon(Icons.image),
              ),
            ),
          ),
        ),
        const SizedBox(height: 8),
        if (_isLoading) const LinearProgressIndicator(minHeight: 4),
        if (_selectedFile != null && !_isLoading)
          Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Seçilen dosya: ${_selectedFile!.title}',
                style: const TextStyle(fontSize: 14, color: Colors.black87),
              ),
              const SizedBox(height: 8),
              if (_previewBytes != null)
                ClipRRect(
                  borderRadius: BorderRadius.circular(8),
                  child: Image.memory(
                    _previewBytes!,
                    height: 150,
                    fit: BoxFit.cover,
                  ),
                ),
            ],
          ),
      ],
    );
  }
}
