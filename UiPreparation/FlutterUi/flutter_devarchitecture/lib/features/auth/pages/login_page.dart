import '/core/theme/custom_colors.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '/core/theme/extensions.dart';
import '../../../core/features/public/constants/public_messages.dart';
import '../../../core/features/public/constants/public_screen_texts.dart';
import '/routes/routes_constants.dart';
import 'package:flutter_modular/flutter_modular.dart';
import '../../../core/bloc/bloc_consumer_extension.dart';
import '../../../core/bloc/bloc_helper.dart';
import '../../../core/widgets/inputs/email_input.dart';
import '../../../core/widgets/inputs/password_input.dart';
import '../../../core/bloc/base_state.dart';
import '../../../core/di/core_initializer.dart';
import '../../../core/features/admin_panel/languages/widgets/language_code_dropdown_button.dart';
import '../bloc/auth_cubit.dart';
import '../../../layouts/base_scaffold.dart';
import '../models/auth.dart';

class LoginPage extends StatelessWidget {
  LoginPage({super.key});
  final GlobalKey<FormState> _form = GlobalKey<FormState>();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  final TextEditingController _languageController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(context, buildLoginForm(context), isDrawer: false);
  }

  Widget buildLoginForm(BuildContext context) {
    return BlocProvider(
      create: (context) => AuthCubit(),
      child: ExtendedBlocConsumer<AuthCubit>(
        listener: (context, state) {
          if (state is BlocFailed) {
            CoreInitializer()
                .coreContainer
                .screenMessage
                .getErrorMessage(state.message);
          }
          if (state is BlocSuccess<String>) {
            CoreInitializer()
                .coreContainer
                .screenMessage
                .getInfoMessage(state.result!);
            Modular.to.navigate(RoutesConstants.appHomePage);
          }
        },
        builder: (context, state) {
          //initial
          if (state is BlocInitial) {
            return _buildResponsiveLogin(context);
          }

          var resultWidget = getResultWidgetByStateWithScaffold(context, state);
          if (resultWidget != null) {
            return resultWidget;
          }

          return _buildResponsiveLogin(context);
        },
      ),
    );
  }

  Widget _buildResponsiveLogin(BuildContext context) {
    return context.isDesktop
        ? Row(
            children: [
              Expanded(flex: 20, child: buildLoginSection(context)),
            ],
          )
        : context.isTablet
            ? Row(
                children: [
                  const Spacer(),
                  Expanded(
                    flex: 8,
                    child: buildLoginSection(context),
                  ),
                  const Spacer(),
                ],
              )
            : Padding(
                padding: context.defaultPadding,
                child: buildLoginSection(context),
              );
  }

  Widget buildLoginSection(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        return ConstrainedBox(
          constraints: BoxConstraints(minHeight: constraints.maxHeight),
          child: IntrinsicHeight(
            child: SingleChildScrollView(
              padding: EdgeInsets.only(
                left: 24,
                right: 24,
                top: 24,
                bottom: MediaQuery.of(context).viewInsets.bottom + 24,
              ),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  const SizedBox(height: 24),
                  Text(
                    "Hoşgeldiniz...",
                    style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
                  ),
                  const SizedBox(height: 24),
                  Text(
                    PublicScreenTexts.artistPortalLoginTitle,
                    style: TextStyle(
                      fontSize: context.isMobile ? 24 : 36,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 24),
                  Form(
                    key: _form,
                    child: Column(
                      children: [
                        CustomEmailInput(
                          context: context,
                          controller: _emailController,
                        ),
                        const SizedBox(height: 16),
                        CustomPasswordInput(
                          context: context,
                          passwordController: _passwordController,
                        ),
                        const SizedBox(height: 16),
                        LanguageCodeDropdownButton(isShort: false),
                      ],
                    ),
                  ),
                  const SizedBox(height: 32),
                  SizedBox(
                    width: double.infinity,
                    child: loginButton(
                      text: PublicScreenTexts.loginButton,
                      onPressed: () async {
                        BlocProvider.of<AuthCubit>(context).emitCheckingState();
                        if (!_form.currentState!.validate()) {
                          BlocProvider.of<AuthCubit>(context).emitFailState(
                            PublicMessages.formValidationErrorMessage,
                          );
                          return;
                        }
                        await BlocProvider.of<AuthCubit>(context).login(
                          AuthRequestBasic(
                            email: _emailController.text,
                            password: _passwordController.text,
                            lang: _languageController.text,
                          ),
                        );
                      },
                    ),
                  ),
                  const SizedBox(height: 32),
                  Text(
                    "100'den fazla sanatçı bize güvenmektedir.",
                    textAlign: TextAlign.center,
                    style: TextStyle(fontSize: 12, fontWeight: FontWeight.bold),
                  ),
                  const SizedBox(height: 8),
                  FutureBuilder<List<String>>(
                    future: _getRandomAvatars(),
                    builder: (context, snapshot) {
                      if (!snapshot.hasData) return const SizedBox();
                      return buildOverlappingAvatars(snapshot.data!);
                    },
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }

  Widget buildLoginElements(BuildContext context) {
    return Padding(
      padding: context.highHorizontalPadding * 2,
      child: Form(
        key: _form,
        child: Column(
          children: [
            Expanded(
              flex: 2,
              child: CustomEmailInput(
                contentPadding: 2,
                context: context,
                controller: _emailController,
              ),
            ),
            Expanded(
              flex: 2,
              child: CustomPasswordInput(
                context: context,
                passwordController: _passwordController,
              ),
            ),
            Expanded(
              flex: 2,
              child: LanguageCodeDropdownButton(
                isShort: false,
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget buildLoginButton(BuildContext context) {
    return Padding(
      padding: context.highHorizontalPadding * 2,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          SizedBox(height: 12),
          SizedBox(
            width: double.infinity,
            child: loginButton(
              text: PublicScreenTexts.loginButton,
              onPressed: () async {
                BlocProvider.of<AuthCubit>(context).emitCheckingState();
                if (!_form.currentState!.validate()) {
                  BlocProvider.of<AuthCubit>(context)
                      .emitFailState(PublicMessages.formValidationErrorMessage);
                  return;
                }
                await BlocProvider.of<AuthCubit>(context)
                    .login(AuthRequestBasic(
                  email: _emailController.text,
                  password: _passwordController.text,
                  lang: _languageController.text,
                ));
              },
            ),
          ),
        ],
      ),
    );
  }

  Widget loginButton({
    required VoidCallback onPressed,
    required String text,
  }) {
    return Container(
      decoration: BoxDecoration(
        color: CustomColors.primary.getColor,
        borderRadius: BorderRadius.circular(8),
      ),
      child: ElevatedButton(
        onPressed: onPressed,
        style: ElevatedButton.styleFrom(
          minimumSize: const Size(double.infinity, 60),
          padding: const EdgeInsets.symmetric(vertical: 12),
          backgroundColor:
              Colors.transparent, // Gradient kullanıldığı için şeffaf
          shadowColor: Colors.transparent,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(8),
          ),
        ),
        child: Text(
          text,
          style: TextStyle(
              color: CustomColors.light.getColor, fontWeight: FontWeight.bold),
        ),
      ),
    );
  }

  Widget buildOverlappingAvatars(List<String> avatars) {
    const double avatarSize = 40;
    const double overlapPercentage = 0.3;
    const double overlapOffset = avatarSize * (1 - overlapPercentage);

    return SizedBox(
      height: avatarSize,
      width: avatarSize + (avatars.length - 1) * overlapOffset,
      child: Stack(
        children: List.generate(avatars.length, (index) {
          return Positioned(
            left: index * overlapOffset,
            child: ClipOval(
              child: Image.asset(
                avatars[index],
                width: avatarSize,
                height: avatarSize,
                fit: BoxFit.cover,
              ),
            ),
          );
        }),
      ),
    );
  }

  Future<List<String>> _getRandomAvatars() async {
    final allAvatars =
        List.generate(9, (index) => 'assets/images/avatar${index + 1}.png');
    allAvatars.shuffle();
    return allAvatars.take(6).toList();
  }

  Widget buildLinkButton(
      {required String label,
      required String title,
      required VoidCallback onTap}) {
    return TextButton(
      style: TextButton.styleFrom(
        backgroundColor: Colors.transparent, // Arka plan tamamen şeffaf
        overlayColor: Colors.transparent, // Tıklama efekti de yok
        padding: EdgeInsets.zero, // Ekstra boşluk da yok
      ),
      onPressed: onTap,
      child: Text(
        label,
        style: const TextStyle(
          fontSize: 14,
          fontWeight: FontWeight.bold,
          color: Colors.white,
          decoration: TextDecoration.underline,
          decorationStyle: TextDecorationStyle.double,
          decorationColor: Colors.white, // Alt çizgi beyaz
        ),
      ),
    );
  }
}
