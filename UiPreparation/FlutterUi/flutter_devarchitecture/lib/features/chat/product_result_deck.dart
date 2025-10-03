import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'models/chat_models.dart';

class ProductResultsDeck extends StatelessWidget {
  const ProductResultsDeck({super.key, required this.products});
  final List<ProductItem> products;

  @override
  Widget build(BuildContext context) {
    if (products.isEmpty) return const SizedBox.shrink();

    return Container(
      margin: const EdgeInsets.only(top: 14),
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface.withOpacity(.55),
        borderRadius: BorderRadius.circular(14),
        border: Border.all(color: Theme.of(context).dividerColor.withOpacity(.2)),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text('Sonuçlar', style: Theme.of(context).textTheme.titleSmall),
          const SizedBox(height: 10),
          LayoutBuilder(
            builder: (context, c) {
              final w = c.maxWidth;
              final cols = w >= 1100 ? 3 : (w >= 740 ? 2 : 1);

              // Kolon başına kart genişliği
              const gap = 12.0;
              final cardWidth = (w - gap * (cols - 1)) / cols;

              // İçerik yüksekliği hedefi (chip + iki satır text + fiyat + butonlar)
              // 3 kolon => daha yüksek kart, tek kolon => biraz daha alçak
              final targetHeight = 400;

              final aspect = cardWidth / targetHeight;

              return GridView.builder(
                itemCount: products.length,
                shrinkWrap: true,
                physics: const NeverScrollableScrollPhysics(),
                gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                  crossAxisCount: cols,
                  crossAxisSpacing: gap,
                  mainAxisSpacing: gap,
                  childAspectRatio: aspect, // <- dinamik
                ),
                itemBuilder: (_, i) => _CartCard(item: products[i]),
              );
            },
          ),
        ],
      ),
    );
  }
}

class _CartCard extends StatelessWidget {
  const _CartCard({required this.item});
  final ProductItem item;

  @override
  Widget build(BuildContext context) {
    final on = Theme.of(context).colorScheme.onSurface;
    return Container(
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: on.withOpacity(.06)),
      ),
      padding: const EdgeInsets.all(12),
      child: Row(
        children: [
          _Thumb(url: item.imageUrl),
          const SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(item.title, maxLines: 1, overflow: TextOverflow.ellipsis, style: const TextStyle(fontWeight: FontWeight.w700)),
                if (item.subtitle.isNotEmpty)
                  Text(item.subtitle, maxLines: 1, overflow: TextOverflow.ellipsis, style: TextStyle(fontSize: 12, color: on.withOpacity(.65))),
                const SizedBox(height: 6),
                Text(_priceLabel(item), style: const TextStyle(fontWeight: FontWeight.w700)),
                const SizedBox(height: 6),
                Wrap(
                  spacing: 6, runSpacing: 6,
                  children: item.pros.take(2).map((e) => _chip(e)).toList(),
                ),
                const Spacer(),
                Row(
                  children: [
                    ElevatedButton.icon(
                      onPressed: item.buyUrl.isEmpty ? null : () async {
                        final uri = Uri.parse(item.buyUrl);
                        if (await canLaunchUrl(uri)) {
                          await launchUrl(uri, mode: LaunchMode.externalApplication);
                        }
                      },
                      icon: const Icon(Icons.shopping_cart_checkout_rounded, size: 16),
                      label: const Text('Satın al'),
                    ),
                    const SizedBox(width: 8),
                    OutlinedButton(
                      onPressed: item.specs.isEmpty ? null : () {
                        showDialog(
                          context: context,
                          builder: (_) => AlertDialog(
                            title: const Text('Özellikler'),
                            content: SizedBox(
                              width: 360,
                              child: Column(
                                mainAxisSize: MainAxisSize.min,
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: item.specs.entries.map((e) => Padding(
                                  padding: const EdgeInsets.symmetric(vertical: 4),
                                  child: Row(
                                    children: [
                                      Expanded(child: Text(e.key, style: const TextStyle(fontWeight: FontWeight.w600))),
                                      const SizedBox(width: 8),
                                      Expanded(child: Text(e.value, textAlign: TextAlign.end)),
                                    ],
                                  ),
                                )).toList(),
                              ),
                            ),
                            actions: [TextButton(onPressed: () => Navigator.pop(context), child: const Text('Kapat'))],
                          ),
                        );
                      },
                      child: const Text('Özellikler'),
                    ),
                  ],
                )
              ],
            ),
          ),
        ],
      ),
    );
  }

  String _priceLabel(ProductItem p) {
    if (p.price <= 0) return 'Fiyat bilgisi yakında';
    return '${p.price.toStringAsFixed(0)} ${p.currency}';
  }

  Widget _chip(String text) => Container(
    padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
    decoration: BoxDecoration(
      borderRadius: BorderRadius.circular(20),
      border: Border.all(color: Colors.black12),
    ),
    child: Row(mainAxisSize: MainAxisSize.min, children: [
      const Icon(Icons.add_rounded, size: 14),
      const SizedBox(width: 4),
      Text(text, style: const TextStyle(fontSize: 12)),
    ]),
  );
}

class _Thumb extends StatelessWidget {
  const _Thumb({required this.url});
  final String url;
  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(8),
      child: Container(
        width: 74,
        height: 74,
        color: Colors.black12,
        child: url.isEmpty
            ? const Icon(Icons.photo, size: 28)
            : Image.network(url, fit: BoxFit.cover, errorBuilder: (_, __, ___) => const Icon(Icons.broken_image)),
      ),
    );
  }
}
