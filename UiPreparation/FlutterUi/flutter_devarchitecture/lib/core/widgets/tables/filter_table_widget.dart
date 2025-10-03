import 'dart:async';
import 'package:flutter/material.dart';
import 'package:collection/collection.dart';

import '/core/widgets/inputs/text_input.dart';
import '../../di/core_initializer.dart';

class FilterTableWidget extends StatefulWidget {
  final List<Map<String, dynamic>> datas;
  final List<Map<String, dynamic>> headers;
  final Color color;
  final List<Widget Function(BuildContext, void Function())>
      customManipulationButton;
  final List<void Function(int)> customManipulationCallback;
  final Widget? infoHover;
  final Widget? addButton;
  final Widget? utilityButton;

  const FilterTableWidget({
    super.key,
    required this.datas,
    required this.headers,
    required this.color,
    required this.customManipulationButton,
    required this.customManipulationCallback,
    this.infoHover,
    this.addButton,
    this.utilityButton,
  });

  @override
  State<FilterTableWidget> createState() => _FilterTableWidgetState();
}

class _FilterTableWidgetState extends State<FilterTableWidget> {
  List<Map<String, dynamic>> filteredData = [];
  final listEquals = const DeepCollectionEquality().equals;

  final TextEditingController controller = TextEditingController();
  Timer? _debounce;

  @override
  void initState() {
    super.initState();
    filteredData = widget.datas;
  }

  @override
  void didUpdateWidget(covariant FilterTableWidget oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (!listEquals(oldWidget.datas, widget.datas)) {
      setState(() {
        filteredData = widget.datas;
      });
    }
  }

  void _onSearchChanged(String value) {
    if (_debounce?.isActive ?? false) _debounce!.cancel();

    _debounce = Timer(const Duration(milliseconds: 300), () {
      final query = value.toLowerCase();
      if (query.isEmpty) {
        setState(() => filteredData = widget.datas);
        return;
      }

      final newData = widget.datas.where((row) {
        final combined =
            row.values.map((e) => e.toString().toLowerCase()).join(" ");
        return combined.contains(query);
      }).toList();

      setState(() => filteredData = newData);
    });
  }

  @override
  void dispose() {
    _debounce?.cancel();
    controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Expanded(
          child: Row(
            children: [
              const Spacer(flex: 10),
              Expanded(
                flex: 2,
                child: CustomTextInput(
                  labelText: " Search",
                  hintText: "",
                  min: 3,
                  max: 15,
                  controller: controller,
                  enabled: true,
                  onChanged: _onSearchChanged,
                ),
              ),
              const Spacer(),
            ],
          ),
        ),
        Expanded(
          flex: 5,
          child: FilteredTableView(
            data: filteredData,
            headers: widget.headers,
            color: widget.color,
            buttons: widget.customManipulationButton,
            callbacks: widget.customManipulationCallback,
            infoHover: widget.infoHover,
            addButton: widget.addButton,
            utilityButton: widget.utilityButton,
          ),
        ),
        const Spacer(),
      ],
    );
  }
}

class FilteredTableView extends StatefulWidget {
  final List<Map<String, dynamic>> data;
  final List<Map<String, dynamic>> headers;
  final Color color;
  final List<Widget Function(BuildContext, void Function())> buttons;
  final List<void Function(int)> callbacks;
  final Widget? infoHover, addButton, utilityButton;

  const FilteredTableView({
    super.key,
    required this.data,
    required this.headers,
    required this.color,
    required this.buttons,
    required this.callbacks,
    this.infoHover,
    this.addButton,
    this.utilityButton,
  });

  @override
  State<FilteredTableView> createState() => _FilteredTableViewState();
}

class _FilteredTableViewState extends State<FilteredTableView> {
  late List<Map<String, dynamic>> currentData;

  @override
  void initState() {
    super.initState();
    currentData = widget.data;
  }

  @override
  void didUpdateWidget(covariant FilteredTableView oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.data != widget.data) {
      setState(() {
        currentData = widget.data;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return CoreInitializer()
        .coreContainer
        .dataTable
        .getTableWithCustomManipulationButtons(
          context,
          widget.headers,
          currentData,
          widget.color,
          widget.buttons,
          widget.callbacks,
          infoHover: widget.infoHover,
          addButton: widget.addButton,
          utilityButton: widget.utilityButton,
        );
  }
}
