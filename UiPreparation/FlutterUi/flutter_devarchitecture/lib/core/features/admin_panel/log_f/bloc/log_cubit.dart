import '/core/di/core_initializer.dart';
import 'package:flutter/foundation.dart';
import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../../di/business_initializer.dart';
import '../models/log.dart';

class LogCubit extends BaseCubit<Log> {
  LogCubit() : super() {
    super.service = BusinessInitializer().businessContainer.logService;
  }

  @override
  Future<void> getAll() async {
    try {
      emit(BlocLoading());
      final result =
          await BusinessInitializer().businessContainer.logService.getLogs();
      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(result.message);
        return null;
      }
      lastSuccessResult = result.data!.map((e) => e.toMap()).toList();
      emit(BlocSuccess<List<Map<String, dynamic>>>(lastSuccessResult));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
