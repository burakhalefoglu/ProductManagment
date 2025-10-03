import 'dart:convert';

import '../abstracts/ai_response.dart';
import '../models/chat_models.dart';

class AIResponseParser implements IAIResponseParser {
  @override
  AIResponse parse(String raw) {
    // Bazı durumlarda model ```json ... ``` içinde döndürebilir; JSON gövdesini ayıklayalım
    final cleaned = _extractJson(raw);
    final map = jsonDecode(cleaned) as Map<String, dynamic>;
    return AIResponse.fromJson(map);
  }

  String _extractJson(String s) {
    final triple = RegExp(r'```(?:json)?\n([\s\S]*?)\n```');
    final m = triple.firstMatch(s);
    if (m != null) return m.group(1)!;

    // Alternatif: metin içinde ilk { ... } bloğunu yakala (kaba ama pratik)
    final first = s.indexOf('{');
    final last = s.lastIndexOf('}');
    if (first != -1 && last != -1 && last > first) {
      return s.substring(first, last + 1);
    }
    // Başarısızsa olduğu gibi döndür (jsonDecode hata atar => üst katman gösterir)
    return s;
  }
}