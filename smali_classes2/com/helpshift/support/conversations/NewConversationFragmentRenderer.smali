.class public Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;
.super Ljava/lang/Object;
.source "NewConversationFragmentRenderer.java"

# interfaces
.implements Lcom/helpshift/conversation/viewmodel/NewConversationRenderer;


# instance fields
.field private final attachmentClearButton:Landroid/widget/ImageButton;

.field private final attachmentContainer:Landroidx/cardview/widget/CardView;

.field private final attachmentFileName:Landroid/widget/TextView;

.field private final attachmentFileSize:Landroid/widget/TextView;

.field private final attachmentImage:Landroid/widget/ImageView;

.field private final context:Landroid/content/Context;

.field private final descriptionField:Lcom/google/android/material/textfield/TextInputEditText;

.field private final descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

.field private final emailField:Lcom/google/android/material/textfield/TextInputEditText;

.field private final emailFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

.field private final menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

.field private final nameField:Lcom/google/android/material/textfield/TextInputEditText;

.field private final nameFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

.field private final newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

.field private final parentView:Landroid/view/View;

.field private final progressBar:Landroid/widget/ProgressBar;


# direct methods
.method constructor <init>(Landroid/content/Context;Lcom/google/android/material/textfield/TextInputLayout;Lcom/google/android/material/textfield/TextInputEditText;Lcom/google/android/material/textfield/TextInputLayout;Lcom/google/android/material/textfield/TextInputEditText;Lcom/google/android/material/textfield/TextInputLayout;Lcom/google/android/material/textfield/TextInputEditText;Landroid/widget/ProgressBar;Landroid/widget/ImageView;Landroid/widget/TextView;Landroid/widget/TextView;Landroidx/cardview/widget/CardView;Landroid/widget/ImageButton;Landroid/view/View;Lcom/helpshift/support/conversations/NewConversationRouter;Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;)V
    .locals 2

    move-object v0, p0

    .line 68
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    move-object v1, p1

    .line 69
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->context:Landroid/content/Context;

    move-object v1, p2

    .line 70
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    move-object v1, p3

    .line 71
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionField:Lcom/google/android/material/textfield/TextInputEditText;

    move-object v1, p4

    .line 72
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    move-object v1, p5

    .line 73
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    move-object v1, p6

    .line 74
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    move-object v1, p7

    .line 75
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    move-object v1, p8

    .line 76
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->progressBar:Landroid/widget/ProgressBar;

    move-object v1, p9

    .line 77
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentImage:Landroid/widget/ImageView;

    move-object v1, p10

    .line 78
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentFileName:Landroid/widget/TextView;

    move-object v1, p11

    .line 79
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentFileSize:Landroid/widget/TextView;

    move-object v1, p12

    .line 80
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentContainer:Landroidx/cardview/widget/CardView;

    move-object v1, p13

    .line 81
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentClearButton:Landroid/widget/ImageButton;

    move-object/from16 v1, p14

    .line 82
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->parentView:Landroid/view/View;

    move-object/from16 v1, p15

    .line 83
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    move-object/from16 v1, p16

    .line 84
    iput-object v1, v0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    return-void
.end method

.method private changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V
    .locals 1

    .line 382
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    if-eqz v0, :cond_0

    .line 383
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->menuItemRenderer:Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;->updateMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    :cond_0
    return-void
.end method

.method private getText(I)Ljava/lang/String;
    .locals 1

    .line 88
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->context:Landroid/content/Context;

    invoke-virtual {v0, p1}, Landroid/content/Context;->getText(I)Ljava/lang/CharSequence;

    move-result-object p1

    invoke-interface {p1}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V
    .locals 1

    .line 377
    invoke-static {p2}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    invoke-virtual {p1, v0}, Lcom/google/android/material/textfield/TextInputLayout;->setErrorEnabled(Z)V

    .line 378
    invoke-virtual {p1, p2}, Lcom/google/android/material/textfield/TextInputLayout;->setError(Ljava/lang/CharSequence;)V

    return-void
.end method


# virtual methods
.method public clearDescriptionError()V
    .locals 2

    .line 108
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public clearEmailError()V
    .locals 2

    .line 138
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public clearNameError()V
    .locals 2

    .line 123
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public disableImageAttachmentClickable()V
    .locals 0

    return-void
.end method

.method public enableImageAttachmentClickable()V
    .locals 0

    return-void
.end method

.method public exit()V
    .locals 1

    .line 316
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/NewConversationRouter;->exitNewConversationView()V

    return-void
.end method

.method public gotoConversation(J)V
    .locals 0

    .line 311
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    invoke-interface {p1}, Lcom/helpshift/support/conversations/NewConversationRouter;->showConversationScreen()V

    return-void
.end method

.method public hideImageAttachmentButton()V
    .locals 2

    .line 143
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method

.method public hideImageAttachmentContainer()V
    .locals 2

    .line 176
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentContainer:Landroidx/cardview/widget/CardView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroidx/cardview/widget/CardView;->setVisibility(I)V

    .line 177
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentImage:Landroid/widget/ImageView;

    invoke-virtual {v0, v1}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 178
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentClearButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setVisibility(I)V

    return-void
.end method

.method public hideProfileForm()V
    .locals 2

    .line 229
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Lcom/google/android/material/textfield/TextInputEditText;->setVisibility(I)V

    .line 230
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0, v1}, Lcom/google/android/material/textfield/TextInputEditText;->setVisibility(I)V

    return-void
.end method

.method public hideProgressBar()V
    .locals 2

    .line 341
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->progressBar:Landroid/widget/ProgressBar;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method

.method public hideStartConversationButton()V
    .locals 2

    .line 306
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method

.method public onAuthenticationFailure()V
    .locals 1

    .line 373
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    invoke-interface {v0}, Lcom/helpshift/support/conversations/NewConversationRouter;->onAuthenticationFailure()V

    return-void
.end method

.method public setDescription(Ljava/lang/String;)V
    .locals 1

    .line 205
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0, p1}, Lcom/google/android/material/textfield/TextInputEditText;->setText(Ljava/lang/CharSequence;)V

    .line 206
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionField:Lcom/google/android/material/textfield/TextInputEditText;

    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0}, Lcom/google/android/material/textfield/TextInputEditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-interface {v0}, Landroid/text/Editable;->length()I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/google/android/material/textfield/TextInputEditText;->setSelection(I)V

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 1

    .line 217
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0, p1}, Lcom/google/android/material/textfield/TextInputEditText;->setText(Ljava/lang/CharSequence;)V

    .line 218
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0}, Lcom/google/android/material/textfield/TextInputEditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-interface {v0}, Landroid/text/Editable;->length()I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/google/android/material/textfield/TextInputEditText;->setSelection(I)V

    return-void
.end method

.method public setEmailRequired()V
    .locals 2

    .line 235
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    sget v1, Lcom/helpshift/R$string;->hs__email_required_hint:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/material/textfield/TextInputEditText;->setHint(Ljava/lang/CharSequence;)V

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 1

    .line 211
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0, p1}, Lcom/google/android/material/textfield/TextInputEditText;->setText(Ljava/lang/CharSequence;)V

    .line 212
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0}, Lcom/google/android/material/textfield/TextInputEditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-interface {v0}, Landroid/text/Editable;->length()I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/google/android/material/textfield/TextInputEditText;->setSelection(I)V

    return-void
.end method

.method public showAttachmentPreviewScreenFromDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 1

    .line 321
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/NewConversationRouter;->showAttachmentPreviewScreenFromDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)V

    return-void
.end method

.method public showConversationStartedMessage()V
    .locals 3

    .line 346
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->context:Landroid/content/Context;

    sget v1, Lcom/helpshift/R$string;->hs__conversation_started_message:I

    const/4 v2, 0x0

    invoke-static {v0, v1, v2}, Lcom/helpshift/views/HSToast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v0

    const/16 v1, 0x10

    .line 349
    invoke-virtual {v0, v1, v2, v2}, Landroid/widget/Toast;->setGravity(III)V

    .line 350
    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    return-void
.end method

.method public showDescriptionEmptyError()V
    .locals 2

    .line 93
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__conversation_detail_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showDescriptionLessThanMinimumError()V
    .locals 2

    .line 98
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__description_invalid_length_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showDescriptionOnlySpecialCharactersError()V
    .locals 2

    .line 103
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->descriptionFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__invalid_description_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showEmailEmptyError()V
    .locals 2

    .line 133
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__invalid_email_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showEmailInvalidError()V
    .locals 2

    .line 128
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__invalid_email_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V
    .locals 1

    .line 368
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->parentView:Landroid/view/View;

    invoke-static {p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Lcom/helpshift/common/exception/ExceptionType;Landroid/view/View;)V

    return-void
.end method

.method public showImageAttachmentButton()V
    .locals 2

    .line 148
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v1, 0x1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method

.method public showImageAttachmentContainer(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Long;)V
    .locals 2
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 159
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->parentView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->isHardwareAccelerated()Z

    move-result v0

    const/4 v1, -0x1

    invoke-static {p1, v1, v0}, Lcom/helpshift/support/util/AttachmentUtil;->getBitmap(Ljava/lang/String;IZ)Landroid/graphics/Bitmap;

    move-result-object p1

    if-eqz p1, :cond_2

    .line 161
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentImage:Landroid/widget/ImageView;

    invoke-virtual {v0, p1}, Landroid/widget/ImageView;->setImageBitmap(Landroid/graphics/Bitmap;)V

    .line 162
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentFileName:Landroid/widget/TextView;

    if-nez p2, :cond_0

    const-string p2, ""

    :cond_0
    invoke-virtual {p1, p2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    const-string p1, ""

    if-eqz p3, :cond_1

    .line 165
    new-instance p1, Lcom/helpshift/support/model/AttachmentFileSize;

    invoke-virtual {p3}, Ljava/lang/Long;->longValue()J

    move-result-wide p2

    long-to-double p2, p2

    invoke-direct {p1, p2, p3}, Lcom/helpshift/support/model/AttachmentFileSize;-><init>(D)V

    invoke-virtual {p1}, Lcom/helpshift/support/model/AttachmentFileSize;->getFormattedFileSize()Ljava/lang/String;

    move-result-object p1

    .line 167
    :cond_1
    iget-object p2, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentFileSize:Landroid/widget/TextView;

    invoke-virtual {p2, p1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 168
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentImage:Landroid/widget/ImageView;

    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 169
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentClearButton:Landroid/widget/ImageButton;

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setVisibility(I)V

    .line 170
    iget-object p1, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->attachmentContainer:Landroidx/cardview/widget/CardView;

    invoke-virtual {p1, p2}, Landroidx/cardview/widget/CardView;->setVisibility(I)V

    :cond_2
    return-void
.end method

.method public showNameEmptyError()V
    .locals 2

    .line 113
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__username_blank_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showNameOnlySpecialCharactersError()V
    .locals 2

    .line 118
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameFieldWrapper:Lcom/google/android/material/textfield/TextInputLayout;

    sget v1, Lcom/helpshift/R$string;->hs__username_blank_error:I

    invoke-direct {p0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->getText(I)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setError(Lcom/google/android/material/textfield/TextInputLayout;Ljava/lang/CharSequence;)V

    return-void
.end method

.method public showProfileForm()V
    .locals 2

    .line 223
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->nameField:Lcom/google/android/material/textfield/TextInputEditText;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/google/android/material/textfield/TextInputEditText;->setVisibility(I)V

    .line 224
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->emailField:Lcom/google/android/material/textfield/TextInputEditText;

    invoke-virtual {v0, v1}, Lcom/google/android/material/textfield/TextInputEditText;->setVisibility(I)V

    return-void
.end method

.method public showProgressBar()V
    .locals 2

    .line 326
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->progressBar:Landroid/widget/ProgressBar;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method

.method public showSearchResultFragment(Ljava/util/ArrayList;)V
    .locals 1

    .line 363
    iget-object v0, p0, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->newConversationRouter:Lcom/helpshift/support/conversations/NewConversationRouter;

    invoke-interface {v0, p1}, Lcom/helpshift/support/conversations/NewConversationRouter;->showSearchResultFragment(Ljava/util/ArrayList;)V

    return-void
.end method

.method public showStartConversationButton()V
    .locals 2

    .line 301
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    const/4 v1, 0x1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method

.method public updateDescriptionErrorState(Lcom/helpshift/widget/TextViewState$TextViewStatesError;)V
    .locals 1

    .line 254
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 255
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showDescriptionEmptyError()V

    goto :goto_0

    .line 257
    :cond_0
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->ONLY_SPECIAL_CHARACTERS:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 258
    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 259
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showDescriptionOnlySpecialCharactersError()V

    goto :goto_0

    .line 261
    :cond_1
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->LESS_THAN_MINIMUM_LENGTH:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 262
    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    .line 263
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showDescriptionLessThanMinimumError()V

    goto :goto_0

    .line 266
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->clearDescriptionError()V

    :goto_0
    return-void
.end method

.method public updateEmailErrorState(Lcom/helpshift/widget/TextViewState$TextViewStatesError;Z)V
    .locals 1

    .line 285
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->INVALID_EMAIL:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 286
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showEmailInvalidError()V

    goto :goto_0

    .line 288
    :cond_0
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 289
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showEmailEmptyError()V

    goto :goto_0

    .line 292
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->clearEmailError()V

    :goto_0
    if-eqz p2, :cond_2

    .line 295
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->setEmailRequired()V

    :cond_2
    return-void
.end method

.method public updateImageAttachmentButton(Z)V
    .locals 1

    .line 153
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method

.method public updateImageAttachmentClick(Z)V
    .locals 0

    if-eqz p1, :cond_0

    .line 196
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->enableImageAttachmentClickable()V

    goto :goto_0

    .line 199
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->disableImageAttachmentClickable()V

    :goto_0
    return-void
.end method

.method public updateImageAttachmentPickerFile(Lcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 2

    if-eqz p1, :cond_1

    .line 183
    iget-object v0, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    .line 187
    :cond_0
    iget-object v0, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->filePath:Ljava/lang/String;

    iget-object v1, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileName:Ljava/lang/String;

    iget-object p1, p1, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileSize:Ljava/lang/Long;

    invoke-virtual {p0, v0, v1, p1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showImageAttachmentContainer(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Long;)V

    goto :goto_1

    .line 184
    :cond_1
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->hideImageAttachmentContainer()V

    :goto_1
    return-void
.end method

.method public updateNameErrorState(Lcom/helpshift/widget/TextViewState$TextViewStatesError;)V
    .locals 1

    .line 272
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->EMPTY:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 273
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showNameEmptyError()V

    goto :goto_0

    .line 275
    :cond_0
    sget-object v0, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->ONLY_SPECIAL_CHARACTERS:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    invoke-virtual {v0, p1}, Lcom/helpshift/widget/TextViewState$TextViewStatesError;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 276
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showNameOnlySpecialCharactersError()V

    goto :goto_0

    .line 279
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->clearNameError()V

    :goto_0
    return-void
.end method

.method public updateProfileForm(Z)V
    .locals 0

    if-eqz p1, :cond_0

    .line 240
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showProfileForm()V

    goto :goto_0

    .line 243
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->hideProfileForm()V

    :goto_0
    return-void
.end method

.method public updateProgressBarVisibility(Z)V
    .locals 0

    if-eqz p1, :cond_0

    .line 332
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->showProgressBar()V

    goto :goto_0

    .line 335
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->hideProgressBar()V

    :goto_0
    return-void
.end method

.method public updateStartConversationButton(Z)V
    .locals 1

    .line 249
    sget-object v0, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/conversations/NewConversationFragmentRenderer;->changeMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V

    return-void
.end method
