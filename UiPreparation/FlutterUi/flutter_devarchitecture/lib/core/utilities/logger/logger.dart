import 'package:logger/logger.dart';
import 'dart:async';
import 'i_logger.dart';

class LoggerImpl implements ILogger {
  LoggerImpl()
      : _logger = Logger(
          printer: CustomPrettyPrinter(),
        );

  final Logger _logger;

  @override
  void logError(String message) {
    _logger.e(_addCallerInfo(message));
  }

  @override
  void logWarning(String message) {
    _logger.w(_addCallerInfo(message));
  }

  @override
  void logInfo(String message) {
    _logger.i(_addCallerInfo(message));
  }

  @override
  void logSuccess(String message) {
    _logger.i(_addCallerInfo('SUCCESS: $message'));
  }

  @override
  void logDebug(String message) {
    _logger.d(_addCallerInfo(message));
  }

  @override
  FutureOr<T> logTraceWithResultAsync<T>(
      String message, FutureOr<T> Function() action) async {
    final start = DateTime.now();
    _logger.d(_addCallerInfo('TRACE Start: $message'));
    final result = await action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d(_addCallerInfo(
        'TRACE End: $message, Duration: ${duration.inMilliseconds} ms'));
    return result;
  }

  @override
  Future<void> logTraceAsync(
      String message, FutureOr<void> Function() action) async {
    final start = DateTime.now();
    _logger.d(_addCallerInfo('TRACE Start: $message'));
    await action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d(_addCallerInfo(
        'TRACE End: $message, Duration: ${duration.inMilliseconds} ms'));
  }

  @override
  T logTraceWithResult<T>(String message, T Function() action) {
    final start = DateTime.now();
    _logger.d(_addCallerInfo('TRACE Start: $message'));
    final result = action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d(_addCallerInfo(
        'TRACE End: $message, Duration: ${duration.inMilliseconds} ms'));
    return result;
  }

  @override
  void logTrace(String message, void Function() action) {
    final start = DateTime.now();
    _logger.d(_addCallerInfo('TRACE Start: $message'));
    action();
    final end = DateTime.now();
    final duration = end.difference(start);
    _logger.d(_addCallerInfo(
        'TRACE End: $message, Duration: ${duration.inMilliseconds} ms'));
  }

  String _addCallerInfo(String message) {
    try {
      final trace = StackTrace.current.toString().split('\n');
      final targetLine = trace.firstWhere(
        (line) =>
            line.contains('LoggerImpl.') && !line.contains('_addCallerInfo'),
        orElse: () => '',
      );

      final match =
          RegExp(r'#\d+\s+.* \((.*):(\d+):\d+\)').firstMatch(targetLine);
      if (match != null) {
        var filePath = match.group(1) ?? '';
        final line = match.group(2);

        if (filePath.contains('package:')) {
          final parts = filePath.split('/');
          final libIndex = parts.indexOf('lib');
          if (libIndex != -1) {
            filePath = parts.sublist(libIndex).join('/');
          }
        }

        return '[$filePath:$line] $message';
      }
    } catch (_) {
      // ignore
    }
    return message;
  }
}

/// Özelleştirilmiş PrettyPrinter
class CustomPrettyPrinter extends PrettyPrinter {
  CustomPrettyPrinter()
      : super(
          methodCount: 0,
          errorMethodCount: 5,
          lineLength: 80,
          colors: true,
          printEmojis: true,
          printTime: true,
        );
}
