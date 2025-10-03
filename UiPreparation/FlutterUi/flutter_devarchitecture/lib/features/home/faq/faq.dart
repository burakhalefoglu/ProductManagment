// ————————————————— FAQ —————————————————
import 'package:flutter/material.dart';


class FaqSection extends StatelessWidget {
  const FaqSection({super.key});
  @override
  Widget build(BuildContext context) {
    return Center(
      child: ConstrainedBox(
        constraints: const BoxConstraints(maxWidth: 1100),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: const [
            _FaqItem(q: 'CompareIt.ai nedir?', a: 'Ürünleri tek aramayla tarafsız kriterlerle kıyaslamayı sağlayan basit bir yardımcıdır.'),
            SizedBox(height: 10),
            _FaqItem(q: 'Veriler nasıl toplanıyor?', a: 'Şimdilik demo verileri kullanılıyor; canlı sürümde güvenilir kaynaklar taranır.'),
            SizedBox(height: 10),
            _FaqItem(q: 'Ücretli mi?', a: 'Beta sürecinde ücretsiz. İleride premium özellikler olabilir.'),
          ],
        ),
      ),
    );
  }
}

class _FaqItem extends StatelessWidget {
  const _FaqItem({required this.q, required this.a});
  final String q;
  final String a;
  @override
  Widget build(BuildContext context) {
    final isDark = Theme.of(context).brightness == Brightness.dark;
    final border = Theme.of(context).colorScheme.onSurface.withOpacity(isDark ? .10 : .14);
    return Container(
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface,
        border: Border.all(color: border),
        borderRadius: BorderRadius.circular(14),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(q, style: const TextStyle(fontFamily: 'Inter', fontWeight: FontWeight.w800)),
          const SizedBox(height: 6),
          Text(a, style: TextStyle(fontWeight: FontWeight.w600, color: Theme.of(context).colorScheme.onSurface.withOpacity(.72))),
        ],
      ),
    );
  }
}

