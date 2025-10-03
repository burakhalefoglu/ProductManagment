import 'package:flutter/material.dart';
import '../../../../constants/core_screen_texts.dart';
import '../user_constants/user_screen_texts.dart';
import '/core/theme/extensions.dart';
import '../../../../widgets/inputs/email_input.dart';
import '../../../../widgets/inputs/phone_input.dart';
import '../../../../widgets/inputs/text_input.dart';
import '../models/user.dart';

class UpdateUserDialog extends StatefulWidget {
  final User user;

  const UpdateUserDialog({Key? key, required this.user}) : super(key: key);

  @override
  _UpdateUserDialogState createState() => _UpdateUserDialogState();
}

class _UpdateUserDialogState extends State<UpdateUserDialog> {
  final _formKey = GlobalKey<FormState>();

  late TextEditingController _emailController;
  late TextEditingController _fullNameController;
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
    _emailController = TextEditingController(text: widget.user.email);
    _fullNameController = TextEditingController(text: widget.user.fullName);
    _mobilePhonesController =
        TextEditingController(text: widget.user.mobilePhones);
    _youtubeProfileController =
        TextEditingController(text: widget.user.youtubeProfile ?? '');
    _instagramProfileController =
        TextEditingController(text: widget.user.instagramProfile ?? '');
    _spotifyProfileController =
        TextEditingController(text: widget.user.spotifyProfile ?? '');
    _videoOrSoundUrlController =
        TextEditingController(text: widget.user.videoOrSoundUrl ?? '');
    _musicStyleController =
        TextEditingController(text: widget.user.musicStyle ?? '');
    _managerController = TextEditingController(text: widget.user.manager ?? '');
    _studioController = TextEditingController(text: widget.user.studio ?? '');
    _inspirationArtistsController =
        TextEditingController(text: widget.user.inspirationArtists ?? '');
    _biographyController = TextEditingController(text: widget.user.biography);
  }

  @override
  void dispose() {
    _emailController.dispose();
    _fullNameController.dispose();
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
      title: Text(UserScreenTexts.updateUsers),
      content: Form(
        key: _formKey,
        child: SizedBox(
          width: context.percent60Screen,
          height: context.percent80Screen,
          child: Column(
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
                  hintText: "Sanatçının kısa biyografisini yazınız",
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
              final updatedUser = User(
                userId: widget.user.userId,
                email: _emailController.text,
                password: widget.user.password,
                fullName: _fullNameController.text,
                status: true, // Kullanıcı her zaman aktif olacak
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
              Navigator.of(context).pop(updatedUser);
            }
          },
          child: Text(CoreScreenTexts.updateButton),
        ),
      ],
    );
  }
}
