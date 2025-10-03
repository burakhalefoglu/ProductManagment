// ————————————————— CTA —————————————————
import 'package:flutter/material.dart';


class CtaSection extends StatelessWidget {
  const CtaSection({super.key});
  @override
  Widget build(BuildContext context) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final glass = isDark
        ? const Color.fromARGB(120, 20, 24, 33)
        : const Color.fromARGB(165, 255, 255, 255);
    final border = Theme.of(context).colorScheme.onSurface.withOpacity(isDark ? .10 : .14);

    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(18),
      decoration: BoxDecoration(
        color: glass,
        border: Border.all(color: border),
        borderRadius: BorderRadius.circular(18),
      ),
      child: Column(
        children: [
          Text('Hemen deneyin', style: Theme.of(context).textTheme.titleLarge?.copyWith(fontFamily: 'Inter', fontWeight: FontWeight.w800)),
          const SizedBox(height: 8),
          Text('Aradığınız iki ürünü yazın ve anında kıyaslayın.', style: TextStyle(color: Theme.of(context).colorScheme.onSurface.withOpacity(.70), fontWeight: FontWeight.w600)),
          const SizedBox(height: 12),
          FilledButton(
            onPressed: () => Scrollable.ensureVisible(
              context,
              duration: const Duration(milliseconds: 250),
            ),
            child: const Text('Başla', style: TextStyle(fontFamily: 'Inter', fontWeight: FontWeight.w700)),
          )
        ],
      ),
    );
  }
}