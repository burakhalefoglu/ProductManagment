// ————————————————— FEATURES —————————————————

import 'package:flutter/material.dart';

class FeaturesSection extends StatelessWidget {
  const FeaturesSection({super.key});

  @override
  Widget build(BuildContext context) {
    final on = Theme.of(context).colorScheme.onSurface;
    return Center(
      child: ConstrainedBox(
        constraints: const BoxConstraints(maxWidth: 1100),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Neden compareit.ai?',
                style: Theme.of(context).textTheme.titleLarge?.copyWith(
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w800,
                )),
            const SizedBox(height: 12),
            LayoutBuilder(builder: (context, c) {
              final w = c.maxWidth;
              final cols = w >= 960 ? 3 : 1;
              return GridView.count(
                crossAxisCount: cols,
                shrinkWrap: true,
                physics: const NeverScrollableScrollPhysics(),
                crossAxisSpacing: 14,
                mainAxisSpacing: 14,
                children: const [
                  _FeatureCard(
                    title: 'Tarafsız öneriler',
                    text: 'Basit ve net; markadan bağımsız değerlendirmeler.',
                  ),
                  _FeatureCard(
                    title: 'Hızlı arama',
                    text: 'Tek sorguda onlarca modeli kıyaslayın.',
                  ),
                  _FeatureCard(
                    title: 'Şeffaf kriterler',
                    text: 'Pil, ekran, fiyat/performans gibi metriklerle.',
                  ),
                ],
              );
            })
          ],
        ),
      ),
    );
  }
}

class _FeatureCard extends StatelessWidget {
  const _FeatureCard({required this.title, required this.text});
  final String title;
  final String text;
  @override
  Widget build(BuildContext context) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final border = Theme.of(context).colorScheme.onSurface.withOpacity(isDark ? .10 : .14);
    return Container(
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface,
        border: Border.all(color: border),
        borderRadius: BorderRadius.circular(16),
        boxShadow: const [BoxShadow(blurRadius: 18, offset: Offset(0, 8), color: Color(0x14000000))],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(title, style: const TextStyle(fontFamily: 'Inter', fontWeight: FontWeight.w800, fontSize: 18)),
          const SizedBox(height: 8),
          Text(text, style: TextStyle(color: Theme.of(context).colorScheme.onSurface.withOpacity(.72), fontWeight: FontWeight.w600)),
        ],
      ),
    );
  }
}

