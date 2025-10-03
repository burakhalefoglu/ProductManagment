import '/core/theme/custom_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '/core/helpers/extensions.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';

class CustomEmailInput extends TextFormField {
  final Color? iconColor;
  CustomEmailInput(
      {required BuildContext context,
      super.key,
      super.onChanged,
      super.enabled = true,
      required super.controller,
      this.iconColor,
      double contentPadding = 20})
      : super(
          validator: (value) {
            if (!enabled) return null;
            if (value == null || value.isEmpty) {
              return CoreScreenTexts.email + " " + CoreMessages.cantBeEmpty;
            }
            if (!value.isValidEmail) {
              return CoreMessages.invalidEmail;
            }

            return null;
          },
          inputFormatters: <TextInputFormatter>[],
          decoration: InputDecoration(
              prefixIcon: Icon(
                Icons.email,
                size: 24,
                color: iconColor ?? CustomColors.dark.getColor,
              ),
              enabled: enabled!,
              hintText: "abc@example.com",
              labelText: CoreScreenTexts.email,
              contentPadding: EdgeInsets.only(bottom: contentPadding)),
        );
}
