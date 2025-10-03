import 'package:flutter/foundation.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../di/core_initializer.dart';
import '../helpers/exceptions.dart';
import 'base_state.dart';
import '../models/i_entity.dart';
import '../services/i_service.dart';

abstract class ICacheSupport<T> {
  List<T>? get lastSuccessResult;
}

class BaseCubit<T extends IEntity> extends Cubit<BaseState>
    implements ICacheSupport<Map<String, dynamic>> {
  late IService service;
  late List<Map<String, dynamic>>? lastSuccessResult = [];

  BaseCubit() : super(const BlocInitial());

  @override
  void emit(BaseState state) {
    if (isClosed) return;
    super.emit(state);
  }

  Future<void> getAll() async {
    try {
      emit(BlocLoading());
      var result = await service.getAll();
      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(result.message);
        return null;
      }
      lastSuccessResult = result.data!;
      emit(BlocSuccess<List<Map<String, dynamic>>>(result.data!));
    } on Exception catch (e) {
      emitFailState("", e: e);
    }
  }

  Future<void> add(T body) async {
    try {
      emit(BlocSending());
      var result = await service.create(body.toMap());
      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(result.message);
        await getAll();
        return;
      }
      emit(BlocAdded());
      getAll();
    } on Exception catch (e) {
      emitFailState("", e: e);
      await getAll();
    }
  }

  Future<void> update(T body) async {
    try {
      emit(BlocSending());
      var result = await service.update(body.toMap());
      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(
          result.message,
        );
        await getAll();
        return;
      }
      emit(BlocUpdated());
      getAll();
    } on Exception catch (e) {
      emitFailState("", e: e);
      await getAll();
    }
  }

  Future<void> delete(dynamic id) async {
    try {
      emit(BlocSending());
      var result = await service.delete(id);
      if (!result.isSuccess) {
        if (kDebugMode) {
          CoreInitializer().coreContainer.logger.logDebug(result.message);
        }
        emitFailState(
          result.message,
        );
        await getAll();
        return;
      }
      emit(BlocDeleted());
      getAll();
    } on Exception catch (e) {
      emitFailState("", e: e);
      await getAll();
    }
  }

  void emitLoadingState() {
    emit(BlocLoading());
  }

  void emitCheckingState() {
    emit(BlocChecking());
  }

  void emitFailState(String message, {Exception? e}) {
    if (kDebugMode) {
      CoreInitializer().coreContainer.logger.logDebug(message);
    }

    if (e != null) {
      if (kDebugMode) {
        CoreInitializer().coreContainer.logger.logDebug(e.toString());
      }
      if (e is NoContentException) {
        emit(BlocFailed(204, message));
      }
      if (e is BadRequestException) {
        emit(BlocFailed(400, message));
      }
      if (e is UnauthorizedException) {
        emit(BlocFailed(401, message));
      }
      if (e is ForbiddenException) {
        emit(BlocFailed(403, message));
      }
      if (e is NotFoundException) {
        emit(BlocFailed(404, message));
      }
      if (e is InternalServerErrorException) {
        emit(BlocFailed(500, message));
      }
    }
    if (kDebugMode) {
      emit(BlocFailed(500, e.toString()));
    } else {
      emit(BlocFailed(500, message));
    }
  }
}
