import 'package:flutter/material.dart';
import 'theme_provider.dart';

enum CustomColors {
  baseDefault,
  transparent,
  primary,   // --accent
  secondary, // mor ton
  success,
  info,
  warning,
  danger,
  light,     // arka plan açık
  gray,      // ikincil metin
  dark,      // birincil metin
  white,
}

extension SelectedColorExtension on CustomColors {
  Color get getColor {
    final isDark = ThemeProvider.isDarkMode();

    // ---- Palette (Tailwind uyumlu) ----
    // Slate
    const slate50  = Color(0xFFF8FAFC);
    const slate100 = Color(0xFFF1F5F9);
    const slate200 = Color(0xFFE2E8F0);
    const slate300 = Color(0xFFCBD5E1);
    const slate400 = Color(0xFF94A3B8);
    const slate500 = Color(0xFF64748B);
    const slate600 = Color(0xFF475569);
    const slate700 = Color(0xFF334155);
    const slate800 = Color(0xFF1F2937);
    const slate900 = Color(0xFF0F172A);

    // Brand / util
    const accentLight = Color(0xFF0F172A); // --accent
    const accentDark  = Color(0xFF38BDF8);

    const secondaryLight = Color(0xFF7C3AED); // purple-600
    const secondaryDark  = Color(0xFFA78BFA); // purple-400

    const successLight = Color(0xFF22C55E);   // emerald-600
    const successDark  = Color(0xFF34D399);   // emerald-400

    const infoLight = Color(0xFF3B82F6);      // blue-600
    const infoDark  = Color(0xFF60A5FA);      // blue-400

    const warningLight = Color(0xFFF59E0B);   // amber-500
    const warningDark  = Color(0xFFFBBF24);   // amber-400

    const dangerLight = Color(0xFFEF4444);    // red-500
    const dangerDark  = Color(0xFFF87171);    // red-400

    switch (this) {
      case CustomColors.transparent:
        return Colors.transparent;

      case CustomColors.primary:
        return isDark ? accentDark : accentLight;

      case CustomColors.secondary:
        return isDark ? secondaryDark : secondaryLight;

      case CustomColors.success:
        return isDark ? successDark : successLight;

      case CustomColors.info:
        return isDark ? infoDark : infoLight;

      case CustomColors.warning:
        return isDark ? warningDark : warningLight;

      case CustomColors.danger:
        return isDark ? dangerDark : dangerLight;

      case CustomColors.light:
      // Arka plan yüzey rengi
        return isDark ? slate800 : slate100;

      case CustomColors.gray:
      // İkincil metin
        return isDark ? slate300 : slate600;

      case CustomColors.dark:
      // Birincil metin rengi
        return isDark ? slate50 : slate900;

      case CustomColors.white:
        return const Color(0xFFFFFFFF);

      case CustomColors.baseDefault:
      // Eğer markanın ana turuncusu olacaksa burada tut.
      // Tam opak hex:
        return const Color(0xFFFF5922);
    }
  }
}
