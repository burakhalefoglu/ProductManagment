abstract class IMessageBroker {
  void getQueue(Function(String message) onChange);
  void close();
  Future<void> queueMessageAsync<T>(T messageModel);
  void queueMessageSync<T>(T messageModel);
}
