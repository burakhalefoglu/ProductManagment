import '/core/widgets/inputs/doc_inputs/custom_file_input.dart';
import '/core/widgets/inputs/doc_inputs/pdf_input.dart';
import '/core/widgets/inputs/doc_inputs/image_input.dart';
import '/core/widgets/inputs/doc_inputs/sound_or_video_input.dart';
import 'package:flutter/material.dart';
import '/core/widgets/inputs/doc_inputs/file_content_model.dart';

class DynamicFileInput extends StatefulWidget {
  final String selectedType;
  final ValueChanged<FileContent?> onFileSelected;

  const DynamicFileInput({
    super.key,
    required this.selectedType,
    required this.onFileSelected,
  });

  @override
  State<DynamicFileInput> createState() => _DynamicFileInputState();
}

class _DynamicFileInputState extends State<DynamicFileInput> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return _buildInputByType();
  }

  Widget _buildInputByType() {
    switch (widget.selectedType) {
      case "PDF":
        return CustomPdfInput(
          labelText: "PDF Belgesi",
          minSizeKB: 1,
          maxSizeKB: 102400,
          onFileSelected: _onFileSelected,
        );
      case "Görsel":
        return CustomImageInput(
          labelText: "Görsel Yükle",
          minSizeKB: 1,
          maxSizeKB: 102400,
          onFileSelected: _onFileSelected,
        );
      case "Ses/Video":
        return CustomMediaInput(
          labelText: "Medya Yükle",
          minSizeKB: 1,
          maxSizeKB: 102400,
          onFileSelected: _onFileSelected,
        );
      case "Dosya":
        return CustomFileInput(
          labelText: "Dosya Yükle (docx, xlsx, txt, vs.)",
          minSizeKB: 1,
          maxSizeKB: 102400,
          onFileSelected: _onFileSelected,
        );
      default:
        return const SizedBox.shrink();
    }
  }

  void _onFileSelected(FileContent? file) {
    widget.onFileSelected(file);
  }
}
