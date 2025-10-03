import 'dart:convert';
import 'dart:io';
import 'dart:math';

import '/core/di/core_initializer.dart';
import '/core/theme/custom_colors.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:http/http.dart' as http;

void refreshPage(BuildContext context) {
  Navigator.pop(context);
  Navigator.pushNamed(
      context, Navigator.of(context).widget.pages.last.name ?? "");
}

Future<String> readResponse(HttpClientResponse response) async {
  final contents = StringBuffer();
  await for (var data in response.transform(utf8.decoder)) {
    contents.write(data);
  }
  return contents.toString();
}

String setToDecimal(double value) {
  var numberFormat = NumberFormat.decimalPattern('tr_TR');
  return numberFormat.format(value).toString();
}

double setDecimalToDouble(String value) {
  var result = value.replaceAll('.', '');
  result = result.replaceAll(' ', '').replaceAll(',', '.');
  return double.tryParse(result.toString()) ?? 0.0;
}

String setShortDecimalTr(String value) {
  var currency = 0.00;
  var numberFormat = NumberFormat.decimalPattern('tr_TR');

  if (value.toString().length < 5) {
    return numberFormat.format(value).toString();
  }
  if (value.toString().length < 7 && value.toString().length >= 5) {
    currency = (int.parse(value) / 1000);
    return "${numberFormat.format(currency)} B";
  }
  if (value.toString().length < 10 && value.toString().length >= 7) {
    currency = (int.parse(value) / 1000000);
    return "${numberFormat.format(currency)} Mn";
  }
  if (value.toString().length < 13 && value.toString().length >= 10) {
    currency = (int.parse(value) / 1000000);
    return "${numberFormat.format(currency)} Mr";
  }
  if (value.toString().length < 16 && value.toString().length >= 13) {
    currency = (int.parse(value) / 1000000);
    return "${numberFormat.format(currency)} Tn";
  }
  return numberFormat.format(value).toString();
}

String parseDateToString(DateTime datetime) {
  return "${datetime.day}-${datetime.month}-${datetime.year}";
}

String getInputControllersText(List<TextEditingController> controllers) {
  String text = "";
  for (var controller in controllers) {
    if (controllers.indexOf(controller) == 0) {
      text = controller.text;
      continue;
    }
    text += " ${controller.text}";
  }
  return text;
}

void clearInputControllers(List<TextEditingController> controllers) {
  for (var controller in controllers) {
    controller.text = "";
    controller.clear();
  }
}

Future<String?> getPublicIP() async {
  try {
    final response = await http.get(Uri.parse('https://api.ipify.org'));
    if (response.statusCode == 200) {
      return response.body;
    } else {
      CoreInitializer()
          .coreContainer
          .logger
          .logError('IP adresi alınamadı: ${response.reasonPhrase}');
      return null;
    }
  } catch (e) {
    CoreInitializer().coreContainer.logger.logError('Hata oluştu: $e');
    return null;
  }
}

String? getBase64FromFile(PlatformFile file) {
  if (file.bytes != null) {
    return base64Encode(file.bytes!);
  }
  return null;
}

String formatAsMarkdown(String content) {
  return content.replaceAllMapped(RegExp(r'(Madde\s+\d+\s+–\s+.+?)\n'),
      (match) {
    return '\n\n### ${match.group(1)}\n';
  }).replaceAllMapped(RegExp(r'(\d\.\d\.\s+.+?)\n'), (match) {
    return '\n**${match.group(1)}**\n';
  });
}

String randomHexString(int length) {
  Random _random = Random();
  StringBuffer sb = StringBuffer();
  for (var i = 0; i < length; i++) {
    sb.write(_random.nextInt(16).toRadixString(16));
  }
  return sb.toString();
}

// Rastgele renk üretici
Color getRandomColor() {
  final random = Random();
  return Color.fromARGB(
    255,
    random.nextInt(256),
    random.nextInt(256),
    random.nextInt(256),
  );
}

final List<Color> barColors = [
  CustomColors.danger.getColor,
  CustomColors.success.getColor,
  CustomColors.warning.getColor,
  CustomColors.primary.getColor,
  CustomColors.secondary.getColor,
  CustomColors.info.getColor,
];
