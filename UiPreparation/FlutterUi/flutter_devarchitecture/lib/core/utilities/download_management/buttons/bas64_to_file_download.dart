import 'dart:convert';
import 'dart:io';
import 'dart:math';
import 'dart:typed_data';
import '/core/di/core_initializer.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:universal_html/html.dart' as html;

Future<void> downloadFileFromBase64(
  String base64String, {
  required String fileName,
}) async {
  try {
    final Uint8List bytes = base64Decode(_cleanBase64(base64String));
    final String safeFileName = fileName.isNotEmpty
        ? fileName
        : 'dosya_${Random().nextInt(9999999)}.bin';

    if (kIsWeb) {
      _saveBase64FileWeb(bytes, safeFileName);
    } else if (Platform.isAndroid || Platform.isIOS) {
      final path = await _getSavePathForMobileApps(safeFileName);
      if (path != null) {
        final file = File(path);
        await file.writeAsBytes(bytes);
      }
    } else if (Platform.isWindows || Platform.isLinux || Platform.isMacOS) {
      final path = await _getSavePathForDesktop(safeFileName);
      if (path != null) {
        final file = File(path);
        await file.writeAsBytes(bytes);
      }
    }
  } catch (e) {
    CoreInitializer().coreContainer.screenMessage.getWarningMessage("Hata: $e");
  }
}

String _cleanBase64(String base64) {
  final regex = RegExp(r'data:.*;base64,');
  return base64.replaceAll(regex, '');
}

void _saveBase64FileWeb(Uint8List bytes, String fileName) {
  final mimeType = _lookupMimeTypeFromFileName(fileName);
  final blob = html.Blob([Uint8List.fromList(bytes)], mimeType);
  final url = html.Url.createObjectUrlFromBlob(blob);
  html.AnchorElement(href: url)
    ..setAttribute("download", fileName)
    ..click();
  html.Url.revokeObjectUrl(url);
}

Future<String?> _getSavePathForMobileApps(String fileName) async {
  String? selectedDirectory = await FilePicker.platform.getDirectoryPath();
  if (selectedDirectory == null) return null;
  return '$selectedDirectory/$fileName';
}

Future<String?> _getSavePathForDesktop(String fileName) async {
  String? selectedDirectory = await FilePicker.platform.getDirectoryPath();
  if (selectedDirectory == null) return null;
  return "$selectedDirectory/$fileName";
}

String _lookupMimeTypeFromFileName(String fileName) {
  final extension = fileName.split('.').last.toLowerCase();
  switch (extension) {
    case 'pdf':
      return 'application/pdf';
    case 'jpg':
    case 'jpeg':
      return 'image/jpeg';
    case 'png':
      return 'image/png';
    case 'gif':
      return 'image/gif';
    case 'mp3':
      return 'audio/mpeg';
    case 'mp4':
      return 'video/mp4';
    case 'doc':
      return 'application/msword';
    case 'docx':
      return 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
    case 'xls':
      return 'application/vnd.ms-excel';
    case 'xlsx':
      return 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    case 'txt':
      return 'text/plain';
    case 'json':
      return 'application/json';
    default:
      return 'application/octet-stream';
  }
}
