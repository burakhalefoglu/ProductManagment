import 'package:flutter/material.dart';
import 'custom_colors.dart';

class ThemeProvider with ChangeNotifier {

  static ThemeMode _themeMode = ThemeMode.light;

  ThemeMode get themeMode => _themeMode;

  void toggleTheme() {
    _themeMode =
        _themeMode == ThemeMode.dark ? ThemeMode.light : ThemeMode.dark;
    notifyListeners();
  }

  static bool isDarkMode() => _themeMode == ThemeMode.dark;
}

const _lightBg      = Color(0xFFF1F5F9); // slate-100  → CSS --bg2 civarı
const _lightSurface = Color(0xFFFFFFFF); // card
const _darkBg       = Color(0xFF0B1220); // slate-950’e yakın koyu arka plan
const _darkSurface  = Color(0xFF111827); // slate-800/850 arası yüzey

final darkTheme = ThemeData(
  useMaterial3: true,
  fontFamily: "Inter",
  brightness: Brightness.dark,

  // Arka plan
  scaffoldBackgroundColor: _darkBg,
  drawerTheme: const DrawerThemeData(backgroundColor: _darkSurface),

  // Renk şeması
  colorScheme: const ColorScheme(
    brightness: Brightness.dark,
    primary: Color(0xFF0EA5E9),  // accent devralıyor
    onPrimary: Colors.black,        // mavi üstünde siyah daha iyi kontrast
    secondary: Color(0xFFA78BFA),   // secondary (dark)
    onSecondary: Colors.black,
    error: Color(0xFFF87171),
    onError: Colors.black,
    background: _darkBg,
    onBackground: Colors.white,
    surface: _darkSurface,
    onSurface: Colors.white,
  ),

  // Kart ve yüzeylerde tint kapalı
  cardTheme: const CardThemeData(
    color: _darkSurface,
    surfaceTintColor: Colors.transparent,
    elevation: 0,
  ),

  // Butonlar
  filledButtonTheme: FilledButtonThemeData(
    style: FilledButton.styleFrom(
      backgroundColor: const Color(0xFF38BDF8), // primary
      foregroundColor: Colors.black,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
    ),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      backgroundColor: Colors.white,
      foregroundColor: const Color(0xFF0F172A), // slate-900
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      elevation: 0,
    ),
  ),
  textButtonTheme: TextButtonThemeData(
    style: TextButton.styleFrom(
      foregroundColor: const Color(0xFF38BDF8),
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
    ),
  ),

  // İkon
  iconTheme: const IconThemeData(color: Colors.white),

  // Inputlar
  inputDecorationTheme: InputDecorationTheme(
    filled: true,
    fillColor: _darkSurface,
    contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 12),
    border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
    enabledBorder: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide(color: CustomColors.gray.getColor),
    ),
    focusedBorder: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide(color: CustomColors.primary.getColor, width: 1.5),
    ),
    labelStyle: TextStyle(color: CustomColors.gray.getColor),
    prefixIconColor: CustomColors.gray.getColor,
  ),
  appBarTheme: const AppBarTheme(
    backgroundColor: Colors.transparent,
    elevation: 0,
    shadowColor: Colors.transparent,
    surfaceTintColor: Colors.transparent,
    scrolledUnderElevation: 0,
    foregroundColor: Colors.white,
  ),
  // Metin
  textTheme: const TextTheme(
    bodyLarge: TextStyle(color: Colors.white, height: 1.5),
    bodyMedium: TextStyle(color: Colors.white, height: 1.5),
    bodySmall: TextStyle(color: Colors.white70),
    headlineLarge: TextStyle(
      color: Colors.white,
      fontSize: 32,
      fontWeight: FontWeight.w700,
      letterSpacing: -0.2,
    ),
    headlineMedium: TextStyle(color: Colors.white, fontSize: 24, fontWeight: FontWeight.w600),
    headlineSmall: TextStyle(color: Colors.white, fontSize: 18, fontWeight: FontWeight.w600),
  ),
);

final lightTheme = ThemeData(
  useMaterial3: true,
  fontFamily: "Inter",
  brightness: Brightness.light,
  appBarTheme: const AppBarTheme(
    backgroundColor: Colors.transparent,
    elevation: 0,
    shadowColor: Colors.transparent,
    surfaceTintColor: Colors.transparent,
    scrolledUnderElevation: 0,
    foregroundColor: Color(0xFF0F172A),
  ),
  scaffoldBackgroundColor: _lightBg,
  drawerTheme: const DrawerThemeData(backgroundColor: _lightSurface),
  colorScheme: const ColorScheme(
    brightness: Brightness.light,
    primary: const Color(0xFF0F172A),   // --ink
    onPrimary: Colors.white,
    secondary: Color(0xFF7C3AED),
    onSecondary: Colors.white,
    error: Color(0xFFEF4444),
    onError: Colors.white,
    background: _lightBg,
    onBackground: Color(0xFF0F172A), // slate-900
    surface: _lightSurface,
    onSurface: Color(0xFF0F172A),
  ),

  cardTheme: const CardThemeData(
    color: _lightSurface,
    surfaceTintColor: Colors.transparent,
    elevation: 0,
  ),

  filledButtonTheme: FilledButtonThemeData(
    style: FilledButton.styleFrom(
      backgroundColor: const Color(0xFF0F172A),
      foregroundColor: Colors.white,
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
    ),
  ),
  elevatedButtonTheme: ElevatedButtonThemeData(
    style: ElevatedButton.styleFrom(
      backgroundColor: Colors.white,
      foregroundColor: const Color(0xFF0F172A),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      elevation: 0,
    ),
  ),
  textButtonTheme: TextButtonThemeData(
    style: TextButton.styleFrom(
      foregroundColor: Colors.white,
      backgroundColor: const Color(0xFF0F172A),
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
    ),
  ),

  iconTheme: IconThemeData(color: CustomColors.primary.getColor),

  inputDecorationTheme: InputDecorationTheme(
    filled: true,
    fillColor: _lightSurface,
    contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 12),
    border: OutlineInputBorder(borderRadius: BorderRadius.circular(12)),
    enabledBorder: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide(color: CustomColors.gray.getColor),
    ),
    focusedBorder: OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
      borderSide: BorderSide(color: CustomColors.primary.getColor, width: 1.5),
    ),
    labelStyle: TextStyle(color: CustomColors.gray.getColor),
    prefixIconColor: CustomColors.gray.getColor,
  ),

  textTheme: const TextTheme(
    bodyLarge: TextStyle(color: Color(0xFF0F172A), height: 1.6),
    bodyMedium: TextStyle(color: Color(0xFF334155), height: 1.6), // slate-700
    bodySmall: TextStyle(color: Color(0xFF475569)),               // slate-600
    headlineLarge: TextStyle(
      color: Color(0xFF0F172A),
      fontSize: 32,
      fontWeight: FontWeight.w700,
      letterSpacing: -0.2,
    ),
    headlineMedium: TextStyle(color: Color(0xFF0F172A), fontSize: 24, fontWeight: FontWeight.w600),
    headlineSmall: TextStyle(color: Color(0xFF0F172A), fontSize: 18, fontWeight: FontWeight.w600),
  ),
);