import 'dart:ui';

import 'package:compare_it/routes/app_route_module.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import '/di/constants_initializer.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:oktoast/oktoast.dart';
import 'package:provider/provider.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'package:flutter_modular/flutter_modular.dart'; // Ensure this import is present

import 'core/constants/core_screen_texts.dart';
import 'core/di/core_initializer.dart';
import 'core/di/firebase/firebase_initializer.dart';
import 'core/extensions/claim_provider.dart';
import 'core/extensions/translation_provider.dart';
import 'core/theme/theme_provider.dart';
import 'di/business_initializer.dart';
import 'package:webview_flutter_web/webview_flutter_web.dart';
import 'package:flutter/foundation.dart'
    show defaultTargetPlatform, TargetPlatform, kIsWeb;
import 'package:webview_flutter_platform_interface/webview_flutter_platform_interface.dart';

Future<void> main() async {
  usePathUrlStrategy();
  WidgetsFlutterBinding.ensureInitialized();
  if (kIsWeb) {
    WebViewPlatform.instance = WebWebViewPlatform();
  } else if (defaultTargetPlatform == TargetPlatform.android) {
    // WebViewPlatform.instance = SurfaceAndroidWebView();
  }
  initializeDateFormatting();
  await injectFirebaseUtils();
  CoreInitializer();
  BusinessInitializer();

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => ThemeProvider()),
        ChangeNotifierProvider(create: (_) => TranslationProvider()),
        ChangeNotifierProvider(create: (_) => ClaimProvider()),
      ],
      child: OKToast(
        child: ModularApp(module: AppRouteModule(), child: AppWidget()),
      ),
    ),
  );
}

Future<void> injectFirebaseUtils() async {
  const isFirebaseEnabled = bool.fromEnvironment('FIREBASE');
  if (isFirebaseEnabled) {
    FirebaseInitializer();
  }
}

class AppWidget extends StatefulWidget {
  const AppWidget({super.key});

  @override
  State<AppWidget> createState() => _AppWidgetState();
}

class _AppWidgetState extends State<AppWidget> {
  bool _translationsLoaded = false;

  @override
  void initState() {
    super.initState();
    _loadTranslations();
  }

  Future<void> _loadTranslations() async {
    await Provider.of<TranslationProvider>(context, listen: false)
        .loadTranslations("tr-TR");

    if (mounted) {
      setState(() {
        _translationsLoaded = true;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    ConstantsInitializer(context);
    return Consumer3<ThemeProvider, TranslationProvider, ClaimProvider>(
      builder: (context, themeProvider, translationProvider, claimProvider, _) {
        return MaterialApp.router(
          title: CoreScreenTexts.appName,
          locale: translationProvider.locale,
          supportedLocales: const [
            Locale('en', 'US'),
            Locale('tr', 'TR'),
            Locale('de', 'DE'),
            Locale('es', 'ES'),
            Locale('fr', 'FR'),
            Locale('it', 'IT'),
            Locale('pt', 'PT'),
            Locale('ru', 'RU'),
            Locale('ja', 'JP'),
            Locale('zh', 'CN'),
          ],
          localizationsDelegates: const [
            GlobalMaterialLocalizations.delegate,
            GlobalWidgetsLocalizations.delegate,
            GlobalCupertinoLocalizations.delegate,
          ],
          scrollBehavior: ScrollConfiguration.of(context).copyWith(
            dragDevices: {
              PointerDeviceKind.touch,
              PointerDeviceKind.mouse,
              PointerDeviceKind.stylus,
              PointerDeviceKind.trackpad,
              PointerDeviceKind.invertedStylus,
              PointerDeviceKind.unknown,
            },
          ),
          theme: lightTheme,
          darkTheme: darkTheme,
          themeMode: themeProvider.themeMode,
          routerConfig: Modular.routerConfig,
          builder: (context, child) {
            if (!_translationsLoaded) {
              return const Scaffold(
                body: Center(child: CircularProgressIndicator()),
              );
            }
            return child!;
          },
        );
      },
    );
  }
}
