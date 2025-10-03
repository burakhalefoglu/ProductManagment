import 'dart:convert';
import '/core/widgets/inputs/doc_inputs/file_content_model.dart';
import 'package:flutter/material.dart';
import 'package:file_picker/file_picker.dart';

class CustomMediaInput extends StatefulWidget {
  final String labelText;
  final int minSizeKB;
  final int maxSizeKB;
  final ValueChanged<FileContent?> onFileSelected;
  final bool enabled;

  const CustomMediaInput({
    super.key,
    required this.labelText,
    required this.minSizeKB,
    required this.maxSizeKB,
    required this.onFileSelected,
    this.enabled = true,
  });

  @override
  State<CustomMediaInput> createState() => _CustomMediaInputState();
}

class _CustomMediaInputState extends State<CustomMediaInput> {
  FileContent? _selectedFile;
  String? _errorText;
  bool _isLoading = false;

  final List<String> _audioExts = ['mp3', 'm4a', 'wav', 'aac', 'ogg', 'flac'];
  final List<String> _videoExts = ['mp4', 'mov', 'avi', 'mkv', 'webm'];

  void _pickFile() async {
    if (!widget.enabled) return;

    setState(() {
      _isLoading = true;
      _errorText = null;
    });

    FilePickerResult? result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: [..._audioExts, ..._videoExts],
      withData: true,
    );

    setState(() {
      _isLoading = false;
    });

    if (result != null && result.files.isNotEmpty) {
      PlatformFile file = result.files.first;
      final ext = file.extension?.toLowerCase();

      if (ext == null || !(_audioExts + _videoExts).contains(ext)) {
        setState(() {
          _errorText = "Geçersiz dosya türü.";
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
          type: file.extension ?? 'bilinmeyen tip',
        );

        setState(() {
          _selectedFile = content;
          _errorText = null;
        });

        widget.onFileSelected(content);
      }
    }
  }

  Icon _getMediaIcon(String? ext) {
    if (ext == null) return const Icon(Icons.insert_drive_file);
    if (_videoExts.contains(ext))
      return const Icon(Icons.videocam, color: Colors.deepPurple);
    if (_audioExts.contains(ext))
      return const Icon(Icons.audiotrack, color: Colors.teal);
    return const Icon(Icons.insert_drive_file);
  }

  String _getMediaLabel(String? ext) {
    if (ext == null) return "Dosya";
    if (_videoExts.contains(ext)) return "Video";
    if (_audioExts.contains(ext)) return "Ses";
    return "Dosya";
  }

  @override
  Widget build(BuildContext context) {
    final ext = _selectedFile?.title.split('.').last.toLowerCase();
    final labelType = _getMediaLabel(ext);

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
                hintText: _selectedFile?.title ?? '$labelType dosyası seçin',
                errorText: _errorText,
                suffixIcon: _getMediaIcon(ext),
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
              'Seçilen $labelType: ${_selectedFile!.title}',
              style: const TextStyle(fontSize: 14, color: Colors.black87),
            ),
          ),
      ],
    );
  }
}
