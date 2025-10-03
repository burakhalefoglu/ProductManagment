import 'package:compare_it/core/theme/extensions.dart';
import 'package:flutter/material.dart';

import '../../../core/theme/custom_colors.dart';
import '../../chat/chat_assist_panel.dart';
import '../../chat/models/chat_models.dart';
import '../../chat/product_result_deck.dart';

class CompareSection extends StatefulWidget {
  const CompareSection({super.key});

  @override
  State<CompareSection> createState() => _CompareSectionState();
}

class _CompareSectionState extends State<CompareSection> {
  List<ProductItem> _products = const [];

  @override
  Widget build(BuildContext context) {
    final isDesktop = context.isDesktop;
    final textTheme = Theme.of(context).textTheme;

    return Center(
      child: ConstrainedBox(
        constraints: const BoxConstraints(maxWidth: 1100),
        child: Padding(
          padding: EdgeInsets.symmetric(vertical: isDesktop ? 48 : 32),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Text(
                'Size uygun ürünü kolayca bulun',
                textAlign: TextAlign.center,
                style: textTheme.headlineLarge?.copyWith(
                  fontFamily: 'Inter',
                  color: CustomColors.primary.getColor.withOpacity(.95),
                  fontSize: 48, // HTML clamp benzeri
                  fontWeight: FontWeight.w700,
                  letterSpacing: -0.2,
                ),
              ),
              const SizedBox(height: 16),

              Text(
                'Dakikalar içinde en doğru karar. Yapay zeka ile hızlı karşılama.',
                textAlign: TextAlign.center,
                style: textTheme.titleMedium?.copyWith(
                  fontFamily: 'Inter',
                  fontWeight: FontWeight.w600,
                  color: Theme.of(context).colorScheme.onSurface.withOpacity(.70),
                ),
              ),
              const SizedBox(height: 20),
              ChatAssistPanel(
                onProductsChanged: (list) => setState(() => _products = list),
              ),
              ProductResultsDeck(products: _products),

            ],
          ),
        ),
      ),
    );
  }
}