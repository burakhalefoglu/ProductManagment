import '/core/bloc/base_state.dart';
import '/core/di/core_initializer.dart';
import 'package:flutter/foundation.dart';

import '../models/translate.dart';
import '../../../../bloc/base_cubit.dart';
import '../../../../../di/business_initializer.dart';

class TranslateCubit extends BaseCubit<Translate> {
  TranslateCubit() : super() {
    super.service = BusinessInitializer().businessContainer.translateService;
  }

  @override
  Future<void> getAll() async {
    try {
      emit(BlocLoading());
      var result = await BusinessInitializer()
          .businessContainer
          .translateService
          .getTranslates();
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
