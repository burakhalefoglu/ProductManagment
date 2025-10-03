import 'package:flutter/material.dart';
import '/core/theme/extensions.dart';

import '../../layouts/base_scaffold.dart';
import '../di/core_initializer.dart';
import 'base_state.dart';

void showScreenMessageByBlocStatus(BaseState state) {
  if (state is BlocFailed) {
    // CoreInitializer()
    //     .coreContainer
    //     .screenMessage
    //     .getErrorMessage(state.message);
  }
  if (state is BlocChecking) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "kontrol ediliyor...");
  }
  if (state is BlocSending) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "Gönderiliyor...");
  }
  if (state is BlocLoading) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getInfoMessage(state.message ?? "Yükleniyor...");
  }
  if (state is BlocAdded) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getSuccessMessage(state.message ?? "veri eklendi.");
  }
  if (state is BlocUpdated) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getSuccessMessage(state.message ?? "veri güncellendi.");
  }
  if (state is BlocDeleted) {
    CoreInitializer()
        .coreContainer
        .screenMessage
        .getWarningMessage(state.message ?? "veri silindi.");
  }
}

Widget? getResultWidgetByStateWithScaffold(
    BuildContext context, BaseState state) {
  var wid = getResultWidgetByState(context, state);
  if (wid != null) {
    return buildBaseScaffold(context, wid);
  }
  return null;
}

Widget? getResultWidgetByState(BuildContext context, BaseState state) {
  // Eğer state success veya failed ise hiçbir şey döndürme (ana widget dönecek)
  if (state is BlocSuccess || state is BlocFailed) {
    return null;
  }

  // Diğer tüm durumlar için animasyon göster (loading, sending, checking vs.)
  return Center(
      child: CoreInitializer()
          .coreContainer
          .statusAnimationAsset
          .getLoadingAnimationAsset(
              context.percent10Screen, context.percent10Screen));
}
