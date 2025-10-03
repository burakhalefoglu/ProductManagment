import '../models/chat_models.dart';

abstract class IChatUseCase {
  Future<AIResponse> analyze(String userText);
}
