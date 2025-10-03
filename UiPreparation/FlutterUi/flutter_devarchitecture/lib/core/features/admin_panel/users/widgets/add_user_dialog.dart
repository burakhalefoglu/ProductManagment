import '/core/widgets/inputs/password_input.dart';
import 'package:flutter/material.dart';
import '/core/constants/core_screen_texts.dart';
import '../user_constants/user_screen_texts.dart';
import '/core/theme/extensions.dart';
import '../../../../widgets/inputs/email_input.dart';
import '../../../../widgets/inputs/phone_input.dart';
import '../../../../widgets/inputs/text_input.dart';
import '../models/user.dart';

class AddUserDialog extends StatefulWidget {
  const AddUserDialog({Key? key}) : super(key: key);

  @override
  _AddUserDialogState createState() => _AddUserDialogState();
}

class _AddUserDialogState extends State<AddUserDialog> {
  final _formKey = GlobalKey<FormState>();

  late TextEditingController _emailController;
  late TextEditingController _fullNameController;
  late TextEditingController _passwordController;
  late TextEditingController _mobilePhonesController;
  late TextEditingController _youtubeProfileController;
  late TextEditingController _instagramProfileController;
  late TextEditingController _spotifyProfileController;
  late TextEditingController _videoOrSoundUrlController;
  late TextEditingController _musicStyleController;
  late TextEditingController _managerController;
  late TextEditingController _studioController;
  late TextEditingController _inspirationArtistsController;
  late TextEditingController _biographyController;

  @override
  void initState() {
    super.initState();
    _emailController = TextEditingController();
    _fullNameController = TextEditingController();
    _passwordController = TextEditingController();
    _mobilePhonesController = TextEditingController();
    _youtubeProfileController = TextEditingController();
    _instagramProfileController = TextEditingController();
    _spotifyProfileController = TextEditingController();
    _videoOrSoundUrlController = TextEditingController();
    _musicStyleController = TextEditingController();
    _managerController = TextEditingController();
    _studioController = TextEditingController();
    _inspirationArtistsController = TextEditingController();
    _biographyController = TextEditingController();
  }

  @override
  void dispose() {
    _emailController.dispose();
    _fullNameController.dispose();
    _passwordController.dispose();
    _mobilePhonesController.dispose();
    _youtubeProfileController.dispose();
    _instagramProfileController.dispose();
    _spotifyProfileController.dispose();
    _videoOrSoundUrlController.dispose();
    _musicStyleController.dispose();
    _managerController.dispose();
    _studioController.dispose();
    _inspirationArtistsController.dispose();
    _biographyController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(UserScreenTexts.addUser),
      content: Form(
        key: _formKey,
        child: Container(
          width: context.percent60Screen,
          height: context.percent80Screen,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Expanded(
                flex: 5,
                child: CustomEmailInput(
                  controller: _emailController,
                  context: context,
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  controller: _fullNameController,
                  labelText: UserScreenTexts.fullName,
                  hintText: UserScreenTexts.fullNameHint,
                  min: 3,
                  max: 100,
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomPhoneInput(
                  controller: _mobilePhonesController,
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomPasswordInput(
                  passwordController: _passwordController,
                  context: context,
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _youtubeProfileController,
                  labelText: "Youtube Profil URL",
                  hintText: "https://youtube.com/...",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _instagramProfileController,
                  labelText: "Instagram Profil URL",
                  hintText: "https://instagram.com/...",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _spotifyProfileController,
                  labelText: "Spotify Profil URL",
                  hintText: "https://spotify.com/...",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _videoOrSoundUrlController,
                  labelText: "Klip Videosu / Ses Kaydı",
                  hintText: "Dosya yolu giriniz veya yükleyiniz",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _musicStyleController,
                  labelText: "Müzik Tarzı",
                  hintText: "Örneğin: Pop, Rock, Jazz",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _managerController,
                  labelText: "Menajer Adı",
                  hintText: "Menajer ismi giriniz",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _studioController,
                  labelText: "Kayıt Stüdyosu",
                  hintText: "Kayıt stüdyosu ismi giriniz",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  min: 0,
                  max: 500,
                  controller: _inspirationArtistsController,
                  labelText: "İlham Aldığı Sanatçılar",
                  hintText: "Sanatçı isimleri giriniz",
                ),
              ),
              const Spacer(),
              Expanded(
                flex: 5,
                child: CustomTextInput(
                  controller: _biographyController,
                  labelText: "Biyografi",
                  hintText: "Sanatçının biyografisini yazınız",
                  min: 20,
                  max: 5000,
                ),
              ),
            ],
          ),
        ),
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: Text(CoreScreenTexts.cancel),
        ),
        ElevatedButton(
          onPressed: () {
            if (_formKey.currentState!.validate()) {
              final newUser = User(
                userId: 0,
                email: _emailController.text,
                fullName: _fullNameController.text,
                password: _passwordController.text,
                status: true, // Varsayılan olarak aktif
                mobilePhones: _mobilePhonesController.text,
                youtubeProfile: _youtubeProfileController.text,
                instagramProfile: _instagramProfileController.text,
                spotifyProfile: _spotifyProfileController.text,
                videoOrSoundUrl: _videoOrSoundUrlController.text,
                musicStyle: _musicStyleController.text,
                manager: _managerController.text,
                studio: _studioController.text,
                inspirationArtists: _inspirationArtistsController.text,
                biography: _biographyController.text,
              );
              Navigator.of(context).pop(newUser);
            }
          },
          child: Text(CoreScreenTexts.saveButton),
        ),
      ],
    );
  }
}
