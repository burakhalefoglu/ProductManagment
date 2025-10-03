import '/core/configs/android_dev_config.dart';

import 'dev_config.dart';
import 'prod_config.dart';
import 'staging_config.dart';

abstract class AppConfig {
  String get apiUrl;
  String get name;
}

//? flutter build web --dart-define ENVIRONMENT=prod --dart-define=FIREBASE=false --release
//? flutter build apk --dart-define ENVIRONMENT=prod --release
//? flutter build windows --dart-define ENVIRONMENT=prod --release --dart-define=FIREBASE=false
//? flutter build macos --dart-define ENVIRONMENT=prod --dart-define=FIREBASE=false --verbose --target-arch=arm64 --release
//? flutter build macos --dart-define ENVIRONMENT=prod --dart-define=FIREBASE=false --verbose --target-arch=x86_64 --release

//? flutter run --dart-define ENVIRONMENT=dev
//? flutter run --dart-define ENVIRONMENT=staging
//? flutter run --dart-define ENVIRONMENT=prod
//? flutter run --dart-define ENVIRONMENT=prod --dart-define=FIREBASE=false
const environmentParameter = String.fromEnvironment('ENVIRONMENT');
AppConfig getConfig() {
  switch (environmentParameter) {
    case 'androidDev':
      return AndroidDevConfig();
    case 'dev':
      return DevConfig();
    case 'staging':
      return StagingConfig();
    case 'prod':
      return ProdConfig();
    default:
      return DevConfig();
  }
}

var appConfig = getConfig();
