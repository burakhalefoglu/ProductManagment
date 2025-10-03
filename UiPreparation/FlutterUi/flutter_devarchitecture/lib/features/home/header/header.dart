// ————————————————— HEADER —————————————————

import 'package:flutter/material.dart';

import '../../../core/theme/custom_colors.dart';

class HeaderBar extends StatelessWidget {
  const HeaderBar({super.key});

  @override
  Widget build(BuildContext context) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final on = Theme.of(context).colorScheme.onSurface;

    final bg = isDark
        ? const Color.fromARGB(140, 16, 18, 24)
        : const Color.fromARGB(190, 255, 255, 255);
    final border = on.withOpacity(isDark ? .06 : .08);

    return Container(
      decoration: BoxDecoration(
        color: bg,
        border: Border.all(color: border, width: 1),
        borderRadius: BorderRadius.circular(14),
        boxShadow: const [
          BoxShadow(blurRadius: 24, offset: Offset(0, 10), color: Color(0x14000000)),
        ],
      ),
      padding: const EdgeInsets.symmetric(horizontal: 14, vertical: 10),
      child: Row(
        children: [
          Container(
            width: 10,
            height: 10,
            decoration: BoxDecoration(
              color: CustomColors.primary.getColor,
              shape: BoxShape.circle,
            ),
          ),
          const SizedBox(width: 10),
          Text(
            'compareit.ai – AI Chat Karşılaştırma',
            style: TextStyle(
              fontFamily: 'Inter',
              fontSize: 18,
              fontWeight: FontWeight.w800,
              color: on.withOpacity(.95),
              letterSpacing: .2,
            ),
          ),
          const Spacer(),
          _NavButton(label: 'Hakkında', onTap: () {}),
          _NavButton(label: 'Nasıl çalışır', onTap: () {}),
          _NavButton(label: 'Giriş', onTap: () {}),
        ],
      ),
    );
  }
}

class _NavButton extends StatelessWidget {
  const _NavButton({required this.label, required this.onTap});
  final String label;
  final VoidCallback onTap;

  @override
  Widget build(BuildContext context) {
    final on = Theme.of(context).colorScheme.onSurface;
    return Padding(
      padding: const EdgeInsets.only(left: 8),
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(10),
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 8),
          child: Text(
            label,
            style: TextStyle(
              fontFamily: 'Inter',
              fontWeight: FontWeight.w700,
              color: on.withOpacity(.75),
            ),
          ),
        ),
      ),
    );
  }
}
