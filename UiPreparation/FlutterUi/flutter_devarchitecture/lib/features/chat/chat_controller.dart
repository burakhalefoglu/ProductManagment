import 'package:flutter/foundation.dart';

import 'abstracts/ai_usecase.dart';
import 'models/chat_models.dart' show ChatMessage, ProductItem, AIOutcomeType;

class ChatController extends ChangeNotifier {
  final IChatUseCase useCase;
  final List<ChatMessage> _messages = [];
  List<ProductItem> _lastProducts = const [];

  // YENİ: Parent’a yükseltmek için notifier
  final ValueNotifier<List<ProductItem>> productsNotifier =
  ValueNotifier<List<ProductItem>>(const []);

  bool _isTyping = false;
  bool get isTyping => _isTyping;
  List<ChatMessage> get messages => List.unmodifiable(_messages);
  List<ProductItem> get products => List.unmodifiable(_lastProducts);

  ChatController({required this.useCase}) {
    _messages.add(ChatMessage(
      role: 'assistant',
      ts: DateTime.now(),
      text:
      'Merhaba 👋 Ben Compareit AI. İhtiyaçlarınızı ve beklentilerinizi paylaşın, sizin için en doğru ürünleri öneriyim.',
    ));
  }

  Future<void> send(String raw) async {
    final input = raw.trim();
    if (input.isEmpty) return;

    _messages.add(ChatMessage(role: 'user', text: input, ts: DateTime.now()));
    _isTyping = true;

    // YENİ: Yeni sorguda ürünleri temizle + haber ver
    _lastProducts = const [];
    productsNotifier.value = _lastProducts;
    notifyListeners();

    try {
      final res = await useCase.analyze(input);

      _isTyping = false;
      _messages.add(ChatMessage(role: 'assistant', text: res.message, ts: DateTime.now()));

      _lastProducts = res.type == AIOutcomeType.recommend ? res.products : const [];
      productsNotifier.value = _lastProducts; // YENİ

      notifyListeners();
    } catch (e) {
      _isTyping = false;
      _messages.add(ChatMessage(role: 'assistant', text: 'Bir hata oluştu: $e', ts: DateTime.now()));
      notifyListeners();
    }
  }

  @override
  void dispose() {
    productsNotifier.dispose(); // YENİ
    super.dispose();
  }
}
