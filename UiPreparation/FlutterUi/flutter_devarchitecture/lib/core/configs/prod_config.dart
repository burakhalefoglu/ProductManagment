import 'app_config.dart';

class ProdConfig implements AppConfig {
  static final ProdConfig _singleton = ProdConfig._internal();

  factory ProdConfig() {
    return _singleton;
  }

  ProdConfig._internal();

  static const String _defaultUrl = 'https://api.isik.media/api/v1';
  static const String _envUrl =
      String.fromEnvironment('URL', defaultValue: _defaultUrl);

  @override
  String get apiUrl => _envUrl;

  @override
  String get name => 'prod';
}
