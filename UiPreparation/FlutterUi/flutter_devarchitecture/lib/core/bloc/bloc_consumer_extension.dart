import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '/core/bloc/base_cubit.dart';
import 'base_state.dart';
import 'bloc_fail_middleware.dart';
import 'bloc_helper.dart';

class ExtendedBlocConsumer<B extends BaseCubit> extends StatelessWidget {
  final BlocWidgetBuilder<BaseState> builder;
  final BlocBuilderCondition<BaseState>? buildWhen;
  final BlocWidgetListener<BaseState>? listener;
  final BlocListenerCondition<BaseState>? listenWhen;

  const ExtendedBlocConsumer({
    Key? key,
    required this.builder,
    this.buildWhen,
    this.listenWhen,
    this.listener,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return BlocConsumer<B, BaseState>(
      buildWhen: buildWhen,
      listenWhen: listenWhen,
      listener: (context, state) {
        BlocFailedMiddleware.handleBlocFailed(context, state);
        showScreenMessageByBlocStatus(state);
        if (listener != null) {
          listener!(context, state);
        }
      },
      builder: (context, state) {
        if (state is! BlocSuccess && state is! BlocInitial) {
          final cubit = context.read<B>();
          final cached = cubit.lastSuccessResult ?? [];

          return builder(
            context,
            BlocSuccess<List<Map<String, dynamic>>>(cached),
          );
        }

        return builder(context, state);
      },
    );
  }
}
