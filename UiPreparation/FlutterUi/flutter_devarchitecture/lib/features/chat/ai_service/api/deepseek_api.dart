// deepseek_api.dart
import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../abstracts/i_ai_service.dart';

// NOT: Buraya kendi anahtarınızı koyun.
const String apiKeyDeepSeek = "";

/// DeepSeek (OpenAI uyumlu) Chat Completions istemcisi
/// JSON çıktısını `response_format` ile zorlamaya çalışır.
/// (Sunucu tarafı destekliyorsa çalışır; desteklenmiyorsa sistem talimatı da bunu pekiştirir.)
class DeepSeekAPI implements IAIService {
  static const String _apiUrl = "https://api.deepseek.com/v1/chat/completions";

  final String systemInstruction;
  final String model;
  final double temperature;
  final double topP;
  final int maxTokens;

  DeepSeekAPI({
    required this.systemInstruction,
    this.model = "deepseek-chat", // Alternatif: "deepseek-reasoner"
    this.temperature = 0.3,
    this.topP = 0.95,
    this.maxTokens = 2048,
  });

  @override
  Future<String> getAnswer(String prompt) async {
    final body = {
      "model": model,
      "messages": [
        {"role": "system", "content": systemInstruction},
        {"role": "user", "content": prompt},
      ],
      // JSON çıktısı isteği — DeepSeek bu alanı OpenAI ile uyumlu olarak destekler.
      // Eğer sunucu reddederse, sistem talimatı yine JSON’ı dayatır.
      "response_format": {"type": "json_object"},
      "temperature": temperature,
      "top_p": topP,
      "max_tokens": maxTokens,
    };

    try {
      final response = await http.post(
        Uri.parse(_apiUrl),
        headers: {
          "Authorization": "Bearer $apiKeyDeepSeek",
          "Content-Type": "application/json",
        },
        body: jsonEncode(body),
      );

      if(response.statusCode == 200) {
        final data = jsonDecode(response.body) as Map<String, dynamic>;
        final msg = data["choices"]?[0]?["message"]?["content"];
        return msg is String ? msg : jsonEncode(msg);
      } else {
        throw Exception("DeepSeek hata: ${response.statusCode} - ${response.body}");
      }
    } catch (e) {
      throw Exception("DeepSeek API isteği başarısız: $e");
    }
  }
}
