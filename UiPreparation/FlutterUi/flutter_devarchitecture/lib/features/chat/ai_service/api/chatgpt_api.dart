// chatgpt_api.dart
import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../abstracts/i_ai_service.dart';

// NOT: Buraya kendi anahtarınızı koyun.
const String apiKeyChatgpt = "";

/// OpenAI Chat Completions tabanlı sade istemci
/// JSON çıktısını `response_format` ile zorlar.
class ChatgptAPI implements IAIService {
  static const String _apiUrl = "https://api.openai.com/v1/chat/completions";

  /// Projede zaten bulunan sistem talimatınızı kullanır.
  /// Örn: final String systemInstruction = "..."; (global ya da DI ile)
  final String systemInstruction;
  final String model;
  final double temperature;
  final double topP;

  ChatgptAPI({
    required this.systemInstruction,
    this.model = "gpt-4o-mini", // İsterseniz "gpt-4o" da kullanabilirsiniz.
    this.temperature = 0.3,
    this.topP = 0.95,
  });

  @override
  Future<String> getAnswer(String prompt) async {
    final body = {
      "model": model,
      "messages": [
        {"role": "system", "content": systemInstruction},
        {"role": "user", "content": prompt},
      ],
      // JSON üretimini zorla
      "response_format": {"type": "json_object"},
      "temperature": temperature,
      "top_p": topP,
    };

    try {
      final response = await http.post(
        Uri.parse(_apiUrl),
        headers: {
          "Authorization": "Bearer $apiKeyChatgpt",
          "Content-Type": "application/json",
        },
        body: jsonEncode(body),
      );

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body) as Map<String, dynamic>;
        final msg = data["choices"]?[0]?["message"]?["content"];
        // Bazı modeller tool/array döndürebilir, string değilse JSON’a çevirip geri veriyoruz.
        return msg is String ? msg : jsonEncode(msg);
      } else {
        // Sunucunun döndürdüğü hatayı ilet
        throw Exception("ChatGPT hata: ${response.statusCode} - ${response.body}");
      }
    } catch (e) {
      // Uygun bir üst katmana fırlatmak genelde daha doğru olur
      throw Exception("ChatGPT API isteği başarısız: $e");
    }
  }
}
