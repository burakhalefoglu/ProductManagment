
import '../abstracts/ai_response.dart';
import '../abstracts/ai_usecase.dart';
import '../abstracts/i_ai_service.dart';
import '../models/chat_models.dart';

class ChatUseCase implements IChatUseCase {
  final IAIService ai;
  final IAIResponseParser parser;
  ChatUseCase({required this.ai, required this.parser});

  @override
  Future<AIResponse> analyze(String userText) async {
    final prompt = _buildPrompt(userText);
    final raw = await ai.getAnswer(prompt);
    final res = parser.parse(raw);
    return res;
  }

  String _buildPrompt(String userText) {
    // Kullanıcı niyetini ilet, gereksiz sistem metni yok; sistem talimatı üstte tanımlı
    return 'Kullanıcı girdisi: \n"""\n$userText\n"""\n'
    'Görev: Girdi yeterliyse 3 ürün öner ve kısa bir özet yaz. Yetersizse eksik bilgiyi tek paragrafta sor.';
  }
}
