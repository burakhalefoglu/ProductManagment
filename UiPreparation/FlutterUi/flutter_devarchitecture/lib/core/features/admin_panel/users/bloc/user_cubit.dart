import '/core/di/core_initializer.dart';
import 'package:flutter/foundation.dart';

import '../../../../../features/auth/models/password_dto.dart';
import '../../../../bloc/base_cubit.dart';
import '../../../../bloc/base_state.dart';
import '../../../../../di/business_initializer.dart';
import '../models/user.dart';

class UserCubit extends BaseCubit<User> {
  UserCubit() : super() {
    super.service = BusinessInitializer().businessContainer.userService;
  }

  Future<void> getUserByUserIdWithToken() async {
    try {
      emit(BlocLoading());
      var result = await BusinessInitializer()
          .businessContainer
          .userService
          .getUserByUserIdWithToken();

      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(result.message);
        return null;
      }
      emit(BlocSuccess<User>(User.fromMap(result.data!)));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }

  Future<void> saveUserPassword(int userId, String password) async {
    emit(BlocLoading());
    try {
      var authService = BusinessInitializer().businessContainer.authService;
      var result = await authService
          .saveUserPassword(PasswordDto(password: password, userId: userId));
      if (!result.isSuccess) {
        emitFailState(result.message);
        return;
      }
      await getAll();
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }
}
