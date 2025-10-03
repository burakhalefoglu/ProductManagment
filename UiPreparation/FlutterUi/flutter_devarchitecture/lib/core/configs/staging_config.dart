import 'dart:io' show HttpOverrides;
import 'package:flutter/foundation.dart' show kIsWeb;

import '/core/helpers/http_override.dart';
import 'app_config.dart';

class StagingConfig implements AppConfig {
  static final StagingConfig _singleton = StagingConfig._internal();

  factory StagingConfig() {
    if (!kIsWeb) {
      HttpOverrides.global = MyHttpOverrides();
    }
    return _singleton;
  }

  StagingConfig._internal();

  static const String _defaultUrl = 'https://localhost:5001/api/v1';
  static const String _envUrl =
      String.fromEnvironment('URL', defaultValue: _defaultUrl);

  @override
  String get apiUrl => _envUrl;

  @override
  String get name => 'staging';
}
