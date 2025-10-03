import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../abstracts/i_ai_service.dart';
import '../systemInstruction.dart';

const apiKeyGoogleGemini = "AIzaSyD_h1KiRRH95IUdl6JSDCmAo77bhU7L9xs";


class GeminiAPI implements IAIService {

  static const String apiUrl =
      'https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=$apiKeyGoogleGemini';

  @override
  Future<String> getAnswer(String prompt) async {
    final body = {
      // Sistem talimatı: JSON zorunluluğu ve şema
      "system_instruction": {
        "role": "system",
        "parts": [
          {
            "text": systemInstruction,
          }
        ]
      },
      "generationConfig": {
        // JSON üretimini zorla
        "response_mime_type": "application/json",
        "temperature": 0.3,
        "topK": 32,
        "topP": 0.95,
      },
      "contents": [
        {
          "role": "user",
          "parts": [
            {"text": prompt}
          ]
        }
      ]
    };

    final response = await http.post(
      Uri.parse(apiUrl),
      headers: {"Content-Type": "application/json"},
      body: jsonEncode(body),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      final text = data["candidates"][0]["content"]["parts"][0]["text"];
      return text is String ? text : jsonEncode(text);
    } else {
      throw Exception("Gemini hata: ${response.statusCode} - ${response.body}");
    }
  }
}