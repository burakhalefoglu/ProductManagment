import 'dart:io';

import '../helpers/http_override.dart';
import 'app_config.dart';

class AndroidDevConfig implements AppConfig {
  static final AndroidDevConfig _singleton = AndroidDevConfig._internal();

  factory AndroidDevConfig() {
    HttpOverrides.global = MyHttpOverrides();
    return _singleton;
  }
  AndroidDevConfig._internal();

  @override
  String get apiUrl =>
      'https://10.0.2.2:5001/api/v1'; // this ip for android testing. Set by backend url

  @override
  String get name => 'dev';
}
