import '/core/bloc/base_state.dart';
import '/core/bloc/bloc_consumer_extension.dart';
import '/core/di/core_initializer.dart';
import '/core/features/admin_panel/users/bloc/user_cubit.dart';
import '/core/features/admin_panel/users/models/user.dart';
import '/core/widgets/inputs/auto_complete.dart';
import 'package:collection/collection.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '/core/theme/extensions.dart';

class CustomUserInput extends StatefulWidget {
  final Function(Map<String, dynamic> selected)? onChanged;
  final TextEditingController controller;
  final double contentPadding;
  final FocusNode focusNode;

  const CustomUserInput({
    super.key,
    this.onChanged,
    required this.controller,
    required this.focusNode,
    required this.contentPadding,
  });

  @override
  State<CustomUserInput> createState() => _CustomUserInputState();
}

class _CustomUserInputState extends State<CustomUserInput> {
  List<User> allUsers = [];

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => UserCubit(),
      child: ExtendedBlocConsumer<UserCubit>(
        listener: (context, state) {
          if (state is BlocFailed) {
            CoreInitializer()
                .coreContainer
                .screenMessage
                .getErrorMessage(state.message);
          }
        },
        builder: (context, state) {
          if (state is BlocInitial) {
            _getUserList(context);
            return const Center(child: CircularProgressIndicator());
          } else if (state is BlocLoading) {
            return const Center(child: CircularProgressIndicator());
          } else if (state is BlocSuccess<List<Map<String, dynamic>>>) {
            for (var i = 0; i < state.result!.length; i++) {}
            allUsers = state.result!.map((e) => User.fromMap(e)).toList();
            return buildDropdown(context);
          }
          return const SizedBox.shrink();
        },
      ),
    );
  }

  SizedBox buildDropdown(BuildContext context) {
    return SizedBox(
      height: context.percent5Screen,
      child: CustomAutoComplete(
        valueKey: "fullName",
        contentPadding: widget.contentPadding,
        isUpperCase: false,
        focusNode: widget.focusNode,
        controller: widget.controller,
        hintText: "Kullanıcı Seçin",
        options: allUsers.map((e) => e.toMap()).toList(),
        labelText: "Kullanıcı Listesi",
        onChanged: (id) {
          var data =
              allUsers.firstWhereOrNull((element) => element.userId == id);
          if (data != null && widget.onChanged != null) {
            widget.onChanged!(data.toMap());
          }
        },
      ),
    );
  }

  Future<void> _getUserList(BuildContext context) async {
    await BlocProvider.of<UserCubit>(context).getAll();
  }
}
