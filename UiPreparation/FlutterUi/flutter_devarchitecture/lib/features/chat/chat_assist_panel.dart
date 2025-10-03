import 'dart:math' as math;

import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';

import 'ai_service/ai_response.dart';
import 'ai_service/api/gemini_api.dart';
import 'ai_service/chat_usecase.dart';
import 'chat_controller.dart';
import 'models/chat_models.dart';

part 'widgets.dart';
class ChatAssistPanel extends StatefulWidget {
  const ChatAssistPanel({
    super.key,
    this.onProductsChanged, // YENİ
  });

  // YENİ: Ürünler güncellenince parent’a haber
  final ValueChanged<List<ProductItem>>? onProductsChanged;

  @override
  State<ChatAssistPanel> createState() => _ChatAssistPanelState();
}

class _ChatAssistPanelState extends State<ChatAssistPanel>
    with SingleTickerProviderStateMixin {
  late final ChatController _vm;
  late final AnimationController _pulse;
  final _controller = TextEditingController();
  final _scrollCtrl = ScrollController();

  @override
  void initState() {
    super.initState();
    _vm = ChatController(useCase: ChatUseCase(ai: GeminiAPI(), parser: AIResponseParser()))
      ..addListener(_onVm);

    // YENİ: Ürün değişimini dinle -> parent’a ilet
    _vm.productsNotifier.addListener(() {
      final prods = _vm.productsNotifier.value;
      widget.onProductsChanged?.call(prods);
    });

    _pulse = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 900),
      lowerBound: .6,
      upperBound: 1.0,
    )..repeat(reverse: true);
  }

  void _onVm() {
    WidgetsBinding.instance.addPostFrameCallback((_) => _scrollToEnd());
    setState(() {});
  }

  @override
  void dispose() {
    _vm.removeListener(_onVm);
    _vm.dispose();
    _controller.dispose();
    _scrollCtrl.dispose();
    _pulse.dispose();
    super.dispose();
  }

  void _scrollToEnd() {
    if (!_scrollCtrl.hasClients) return;
    _scrollCtrl.animateTo(
      _scrollCtrl.position.maxScrollExtent + 180,
      duration: const Duration(milliseconds: 300),
      curve: Curves.easeOutCubic,
    );
  }

  void _send() {
    if (_controller.text.isEmpty) return;
    final text = _controller.text;
    _controller.clear();
    _vm.send(text);
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isDark = theme.brightness == Brightness.dark;

    final glassBg = isDark
        ? const Color.fromARGB(120, 18, 20, 26)
        : const Color.fromARGB(225, 255, 255, 255);
    final borderColor = theme.colorScheme.onSurface.withOpacity(isDark ? .08 : .12);
    final panelHeight = MediaQuery.of(context).size.width > 1000 ? 560.0 : 480.0;

    return ClipRRect(
      borderRadius: BorderRadius.circular(18),
      child: Container(
        height: panelHeight,
        decoration: BoxDecoration(
          color: glassBg,
          border: Border.all(color: borderColor),
          boxShadow: const [BoxShadow(blurRadius: 18, offset: Offset(0, 8), color: Color(0x14000000))],
        ),
        child: Column(
          children: [
            _Header(isDark: isDark),
            const Divider(height: 0.3),
            Expanded(
              child: ListView(
                controller: _scrollCtrl,
                padding: const EdgeInsets.fromLTRB(14, 12, 14, 10),
                children: [
                  _MessagesList(messages: _vm.messages, pulse: _pulse),
                  if (_vm.isTyping) const _TypingRowSpacer(),
                ],
              ),
            ),
            const Divider(height: 0.3),
            _Composer(controller: _controller, onSend: _send, isBusy: _vm.isTyping),
          ],
        ),
      ),
    );
  }
}