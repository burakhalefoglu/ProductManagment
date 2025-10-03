part of 'chat_assist_panel.dart';

// --------------------
// HEADER & ICONS
// --------------------
class _Header extends StatelessWidget {
  const _Header({required this.isDark});
  final bool isDark;

  @override
  Widget build(BuildContext context) {
    final onSurface = Theme.of(context).colorScheme.onSurface.withOpacity(.72);
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 14, vertical: 10),
      child: Row(
        children: [
          const _GlowDot(),
          const SizedBox(width: 10),
          Text(
            'Asistan',
            style: TextStyle(fontSize: 15, fontWeight: FontWeight.w600, color: onSurface),
          ),
          const Spacer(),
          const _IconBtn(icon: Icons.auto_awesome, tooltip: 'Örnekler'),
          const _IconBtn(icon: Icons.tune_rounded, tooltip: 'Ayarlar'),
        ],
      ),
    );
  }
}

class _GlowDot extends StatelessWidget {
  const _GlowDot();
  @override
  Widget build(BuildContext context) {
    final color = Theme.of(context).colorScheme.primary;
    return Container(
      width: 12,
      height: 12,
      decoration: BoxDecoration(
        shape: BoxShape.circle,
        boxShadow: [BoxShadow(color: color.withOpacity(.35), blurRadius: 16, spreadRadius: 2)],
        gradient: RadialGradient(colors: [color, color.withOpacity(.5)]),
      ),
    );
  }
}

class _IconBtn extends StatelessWidget {
  const _IconBtn({required this.icon, required this.tooltip});
  final IconData icon;
  final String tooltip;
  @override
  Widget build(BuildContext context) {
    final onSurface = Theme.of(context).colorScheme.onSurface.withOpacity(.70);
    return Padding(
      padding: const EdgeInsets.only(left: 4),
      child: Tooltip(
        message: tooltip,
        child: InkWell(
          borderRadius: BorderRadius.circular(10),
          onTap: () {},
          child: Container(
            padding: const EdgeInsets.all(8),
            decoration: BoxDecoration(
              borderRadius: BorderRadius.circular(10),
              color: onSurface.withOpacity(.05),
            ),
            child: Icon(icon, size: 18, color: onSurface),
          ),
        ),
      ),
    );
  }
}

// --------------------
// MESSAGES & BUBBLES
// --------------------
class _MessagesList extends StatelessWidget {
  const _MessagesList({required this.messages, required this.pulse});
  final List<ChatMessage> messages;
  final AnimationController pulse;

  @override
  Widget build(BuildContext context) {
    return ListView.separated(
      physics: const NeverScrollableScrollPhysics(),
      shrinkWrap: true,
      itemCount: messages.length,
      separatorBuilder: (_, __) => const SizedBox(height: 8),
      itemBuilder: (context, i) {
        final m = messages[i];
        final isUser = m.role == 'user';
        if (m.isTyping) {
          return Align(alignment: Alignment.centerLeft, child: _TypingBubble(pulse: pulse));
        }
        return Align(
          alignment: isUser ? Alignment.centerRight : Alignment.centerLeft,
          child: _Bubble(text: m.text, isUser: isUser),
        );
      },
    );
  }
}

class _TypingRowSpacer extends StatelessWidget {
  const _TypingRowSpacer();
  @override
  Widget build(BuildContext context) => const SizedBox(height: 8);
}

class _TypingBubble extends StatelessWidget {
  const _TypingBubble({required this.pulse});
  final AnimationController pulse;
  @override
  Widget build(BuildContext context) {
    final bg = Theme.of(context).colorScheme.surface.withOpacity(.55);
    final br = BorderRadius.circular(14);
    return Row(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: [
        const _Avatar(isUser: false),
        const SizedBox(width: 8),
        ScaleTransition(
          scale: pulse,
          child: Container(
            padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
            decoration: BoxDecoration(
              color: bg,
              borderRadius: br,
              border: Border.all(color: Colors.white.withOpacity(.06)),
            ),
            child: const _TypingDots(),
          ),
        ),
      ],
    );
  }
}

class _TypingDots extends StatefulWidget {
  const _TypingDots();
  @override
  State<_TypingDots> createState() => _TypingDotsState();
}

class _TypingDotsState extends State<_TypingDots> with SingleTickerProviderStateMixin {
  late final AnimationController _c;
  @override
  void initState() {
    super.initState();
    _c = AnimationController(vsync: this, duration: const Duration(milliseconds: 900))..repeat();
  }

  @override
  void dispose() {
    _c.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _c,
      builder: (context, _) {
        final t = _c.value;
        double d(int i) => 0.3 * (1 + math.sin(t * 2 * math.pi + i));
        return Row(children: [
          _dot(d(0)),
          const SizedBox(width: 4),
          _dot(d(1)),
          const SizedBox(width: 4),
          _dot(d(2)),
        ]);
      },
    );
  }

  Widget _dot(double scale) => Transform.scale(scale: .8 + scale * .4, child: const _Dot());
}

class _Dot extends StatelessWidget {
  const _Dot();
  @override
  Widget build(BuildContext context) {
    return Container(
      width: 6,
      height: 6,
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.onSurface.withOpacity(.55),
        shape: BoxShape.circle,
      ),
    );
  }
}

class _Avatar extends StatelessWidget {
  const _Avatar({required this.isUser});
  final bool isUser;
  @override
  Widget build(BuildContext context) {
    final base = Theme.of(context).colorScheme.onSurface.withOpacity(.10);
    return Container(
      width: 28,
      height: 28,
      decoration: BoxDecoration(shape: BoxShape.circle, color: base),
      child: Icon(
        isUser ? Icons.person : Icons.smart_toy_rounded,
        size: 16,
        color: Theme.of(context).colorScheme.onSurface.withOpacity(.72),
      ),
    );
  }
}

class _Bubble extends StatelessWidget {
  const _Bubble({required this.text, required this.isUser});
  final String text;
  final bool isUser;
  @override
  Widget build(BuildContext context) {
    final scheme = Theme.of(context).colorScheme;
    final isDark = Theme.of(context).brightness == Brightness.dark;

    final Color userBg = isDark
        ? const Color.fromARGB(170, 34, 145, 96)
        : const Color.fromARGB(210, 230, 248, 239);
    final Color botBg = scheme.surface.withOpacity(.55);

    final br = BorderRadius.only(
      topLeft: const Radius.circular(14),
      topRight: const Radius.circular(14),
      bottomLeft: Radius.circular(isUser ? 14 : 4),
      bottomRight: Radius.circular(isUser ? 4 : 14),
    );

    final bubble = Container(
      constraints: const BoxConstraints(maxWidth: 720),
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
      decoration: BoxDecoration(
        color: isUser ? userBg : botBg,
        borderRadius: br,
        border: Border.all(color: Colors.white.withOpacity(.06)),
      ),
      child: _RichTextContent(text: text, isUser: isUser),
    );

    if (isUser) {
      return Row(
        mainAxisSize: MainAxisSize.min,
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          Flexible(child: bubble),
          const SizedBox(width: 8),
          const _Avatar(isUser: true),
        ],
      );
    } else {
      return Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          const _Avatar(isUser: false),
          const SizedBox(width: 8),
          Flexible(child: bubble),
        ],
      );
    }
  }
}

class _RichTextContent extends StatelessWidget {
  const _RichTextContent({required this.text, required this.isUser});
  final String text;
  final bool isUser;

  @override
  Widget build(BuildContext context) {
    final on = Theme.of(context).colorScheme.onSurface;
    final style = TextStyle(height: 1.32, fontSize: 14.5, color: isUser ? on.withOpacity(.95) : on.withOpacity(.90));

    final spans = <TextSpan>[];
    final lines = text.split('\n');

    for (var li in lines) {
      if (li.startsWith('• ')) {
        spans.add(TextSpan(text: '• ', style: style.copyWith(fontWeight: FontWeight.w600)));
        spans.addAll(_inlineSpans(li.substring(2), style));
      } else if (li.startsWith('→ ')) {
        spans.add(TextSpan(text: '→ ', style: style.copyWith(fontWeight: FontWeight.w700)));
        spans.addAll(_inlineSpans(li.substring(2), style));
      } else {
        spans.addAll(_inlineSpans(li, style));
      }
      spans.add(const TextSpan(text: '\n'));
    }

    if (spans.isNotEmpty) spans.removeLast();

    return SelectableText.rich(TextSpan(children: spans));
  }

  List<TextSpan> _inlineSpans(String src, TextStyle base) {
    final spans = <TextSpan>[];
    final regex = RegExp(r'(\*\*[^*]+\*\*|`[^`]+`|\[[^\]]+\]\([^\)]+\))');
    int idx = 0;

    for (final m in regex.allMatches(src)) {
      if (m.start > idx) {
        spans.add(TextSpan(text: src.substring(idx, m.start), style: base));
      }
      final token = src.substring(m.start, m.end);
      if (token.startsWith('**')) {
        spans.add(TextSpan(text: token.substring(2, token.length - 2), style: base.copyWith(fontWeight: FontWeight.w700)));
      } else if (token.startsWith('`')) {
        spans.add(TextSpan(
          text: token.substring(1, token.length - 1),
          style: base.copyWith(fontFamily: 'monospace', backgroundColor: Colors.black.withOpacity(.07)),
        ));
      } else if (token.startsWith('[')) {
        final label = RegExp(r'\[([^\]]+)\]').firstMatch(token)?.group(1) ?? 'link';
        spans.add(TextSpan(text: label, style: base.copyWith(decoration: TextDecoration.underline)));
      }
      idx = m.end;
    }
    if (idx < src.length) {
      spans.add(TextSpan(text: src.substring(idx), style: base));
    }
    return spans;
  }
}

// --------------------
// COMPOSER
// --------------------
class _Composer extends StatefulWidget {
  const _Composer({
    required this.controller,
    required this.onSend,
    this.isBusy = false,
  });

  final TextEditingController controller;
  final VoidCallback onSend;
  final bool isBusy;

  @override
  State<_Composer> createState() => _ComposerState();
}

class _ComposerState extends State<_Composer> {
  @override
  Widget build(BuildContext context) {
    final on = Theme.of(context).colorScheme.onSurface;
    return Padding(
      padding: const EdgeInsets.fromLTRB(10, 8, 10, 12),
      child: Row(
        children: [
          _roundBtn(Icons.add, tooltip: 'Ekle', onTap: () {}),
          const SizedBox(width: 8),
          Expanded(
            child: AnimatedContainer(
              duration: const Duration(milliseconds: 180),
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
              child: ConstrainedBox(
                constraints: const BoxConstraints(maxHeight: 160),
                child: Scrollbar(
                  child: TextField(
                    controller: widget.controller,
                    minLines: 1,
                    maxLines: 8,
                    decoration: InputDecoration(
                      isCollapsed: true,
                      border: InputBorder.none,
                      hintText: 'Mesaj yazın…',
                      hintStyle: TextStyle(color: on.withOpacity(.45)),
                    ),
                    onSubmitted: (_) {
                      widget.onSend();
                      widget.controller.clear();
                    },
                  ),
                ),
              ),
            ),
          ),
          const SizedBox(width: 8),
          AnimatedSwitcher(
            duration: const Duration(milliseconds: 150),
            child: widget.isBusy
                ? _roundBtn(Icons.stop_circle_outlined, tooltip: 'Durdur', onTap: () {})
                : _roundBtn(Icons.send_rounded, tooltip: 'Gönder', onTap: widget.onSend),
          ),
        ],
      ),
    );
  }

  Widget _roundBtn(IconData icon, {required String tooltip, required VoidCallback onTap}) {
    final on = Theme.of(context).colorScheme.onSurface;
    return Tooltip(
      message: tooltip,
      child: InkResponse(
        onTap: onTap,
        radius: 22,
        child: Container(
          width: 38,
          height: 38,
          decoration: BoxDecoration(
            shape: BoxShape.circle,
            color: on.withOpacity(.08),
            border: Border.all(color: Colors.white.withOpacity(.06)),
          ),
          child: Icon(icon, size: 18, color: on.withOpacity(.80)),
        ),
      ),
    );
  }
}
