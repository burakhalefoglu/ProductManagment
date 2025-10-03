import '/core/theme/extensions.dart';
import '/layouts/base_scaffold.dart';
import 'package:flutter/material.dart';

import 'compare_section/compare_section.dart';
import 'cta/cta.dart';
import 'faq/faq.dart';
import 'feature/feature.dart';
import 'footer/footer.dart';
import 'header/header.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});
  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      SafeArea(
        child: SingleChildScrollView(
          padding: context.isDesktop
              ? context.highHorizontalPadding * 1.5
              : context.lowHorizontalPadding,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: const [
              HeaderBar(),
              SizedBox(height: 22),
              CompareSection(),
              SizedBox(height: 28),
              FeaturesSection(),
              SizedBox(height: 28),
              FaqSection(),
              SizedBox(height: 28),
              CtaSection(),
              SizedBox(height: 16),
              FooterSection(),
            ],
          ),
        ),
      ),
    );
  }
}