class ChatMessage {
  final String role; // 'user' | 'assistant'
  final String text;
  final DateTime ts;
  final bool isTyping; // for assistant typing indicator
  const ChatMessage({
    required this.role,
    required this.text,
    required this.ts,
    this.isTyping = false,
  });
}

enum AIOutcomeType { ask, recommend }

class ProductItem {
  final String id;
  final String title;
  final String subtitle;
  final String imageUrl;
  final String buyUrl;
  final String currency;
  final double price;
  final List<String> pros;
  final List<String> cons;
  final Map<String, String> specs; // key:value kısa teknikler

  const ProductItem({
    required this.id,
    required this.title,
    required this.subtitle,
    required this.imageUrl,
    required this.buyUrl,
    required this.currency,
    required this.price,
    required this.pros,
    required this.cons,
    required this.specs,
  });

  factory ProductItem.fromJson(Map<String, dynamic> j) {
    return ProductItem(
      id: j['id']?.toString() ?? '',
      title: j['title'] ?? j['name'] ?? '',
      subtitle: j['subtitle'] ?? (j['brand'] ?? ''),
      imageUrl: j['imageUrl'] ?? j['image_url'] ?? '',
      buyUrl: j['buyUrl'] ?? j['buy_url'] ?? '',
      currency: j['currency'] ?? 'USD',
      price: (j['price'] is num) ? (j['price'] as num).toDouble() : double.tryParse(j['price']?.toString() ?? '') ?? 0,
      pros: (j['pros'] as List?)?.map((e) => e.toString()).toList() ?? const [],
      cons: (j['cons'] as List?)?.map((e) => e.toString()).toList() ?? const [],
      specs: (j['specs'] as Map?)?.map((k, v) => MapEntry(k.toString(), v.toString())) ?? const {},
    );
  }
}

class AIResponse {
  final AIOutcomeType type;
  final String message; // asistanın kullanıcıya verdiği metin (soru/öneri özeti)
  final List<ProductItem> products; // 0-3 ürün
  const AIResponse({required this.type, required this.message, required this.products});

  factory AIResponse.fromJson(Map<String, dynamic> j) {
    final typeStr = (j['type'] ?? '').toString().toLowerCase();
    final t = typeStr == 'recommend' ? AIOutcomeType.recommend : AIOutcomeType.ask;
    final prods = (j['products'] as List?)?.map((e) => ProductItem.fromJson(e as Map<String, dynamic>)).toList() ?? const <ProductItem>[];
    return AIResponse(type: t, message: (j['message'] ?? '').toString(), products: prods);
  }
}