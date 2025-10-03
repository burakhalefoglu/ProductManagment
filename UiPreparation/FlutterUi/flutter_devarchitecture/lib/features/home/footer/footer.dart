import 'package:flutter/material.dart';

class FooterSection extends StatelessWidget {
  const FooterSection({super.key});

  @override
  Widget build(BuildContext context) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final glassBg = isDark
        ? const Color.fromARGB(100, 20, 24, 33)   // ~rgba(20,24,33,.40)
        : const Color.fromARGB(128, 255, 255, 255); // ~rgba(255,255,255,.50)
    final borderColor = isDark
        ? Colors.white.withOpacity(.06)
        : Colors.black.withOpacity(.08);
    final ink3 = Theme.of(context).colorScheme.onSurface.withOpacity(.55);

    return Container(
      decoration: BoxDecoration(
        color: glassBg,
        border: Border(top: BorderSide(color: borderColor, width: 1)),
      ),
      padding: const EdgeInsets.symmetric(vertical: 20),
      child: Center(
        child: ConstrainedBox(
          constraints: const BoxConstraints(maxWidth: 1100),
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 14),
            child: Text(
              '© ${DateTime.now().year} CompareIt.ai — Tüm hakları saklıdır.',
              textAlign: TextAlign.center,
              style: TextStyle(
                color: ink3,
                fontWeight: FontWeight.w600,
                fontFamily: 'Inter',
              ),
            ),
          ),
        ),
      ),
    );
  }
}