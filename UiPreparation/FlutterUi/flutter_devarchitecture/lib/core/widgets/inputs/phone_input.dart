import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '/core/helpers/extensions.dart';
import '../../constants/core_messages.dart';
import '../../constants/core_screen_texts.dart';

class CustomPhoneInput extends TextFormField {
  CustomPhoneInput({
    super.key,
    super.onChanged,
    required super.controller,
    super.enabled = true,
  }) : super(
          validator: (value) {
            if (!enabled!) return null;
            if (value == null || value.isEmpty) {
              return CoreScreenTexts.phoneNumber + CoreMessages.cantBeEmpty;
            }
            if (!value.isValidPhone) {
              return CoreMessages.invalidPhone;
            }
            return null;
          },
          inputFormatters: <TextInputFormatter>[],
          decoration: InputDecoration(
              labelText: CoreScreenTexts.phoneNumber,
              hintText: '05555555555',
              contentPadding: EdgeInsets.only(bottom: 20)),
        );
}
