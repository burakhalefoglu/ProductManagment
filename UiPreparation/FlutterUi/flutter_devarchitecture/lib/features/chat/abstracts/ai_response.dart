
import '../models/chat_models.dart';

abstract class IAIResponseParser {
  AIResponse parse(String raw);
}