.class public Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;
.super Landroid/app/Fragment;
.source "SupportFormFragment.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;,
        Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;,
        Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;
    }
.end annotation


# static fields
.field private static final SELECT_PICTURE_REQUEST_CODE:I = 0x1389


# instance fields
.field private attachment:Landroid/net/Uri;

.field private attachmentView:Landroid/widget/TextView;

.field private context:Landroid/content/Context;

.field private removeAttachmentView:Landroid/view/View;

.field private supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

.field private uiContext:Lnet/gogame/gowrap/ui/UIContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 41
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    const/4 v0, 0x0

    .line 44
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachmentView:Landroid/widget/TextView;

    .line 45
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->removeAttachmentView:Landroid/view/View;

    .line 46
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachment:Landroid/net/Uri;

    .line 47
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    .line 48
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

    .line 49
    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/ui/UIContext;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Lnet/gogame/gowrap/support/SupportCategory;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

    return-object p0
.end method

.method static synthetic access$102(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/support/SupportCategory;)Lnet/gogame/gowrap/support/SupportCategory;
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->supportCategory:Lnet/gogame/gowrap/support/SupportCategory;

    return-object p1
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/net/Uri;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachment:Landroid/net/Uri;

    return-object p0
.end method

.method static synthetic access$202(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/net/Uri;)Landroid/net/Uri;
    .locals 0

    .line 41
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachment:Landroid/net/Uri;

    return-object p1
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    return-object p0
.end method

.method static synthetic access$400(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/view/View;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->removeAttachmentView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$500(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/widget/TextView;
    .locals 0

    .line 41
    iget-object p0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachmentView:Landroid/widget/TextView;

    return-object p0
.end method

.method private getFilename(Landroid/net/Uri;)Ljava/lang/String;
    .locals 8

    .line 275
    invoke-virtual {p1}, Landroid/net/Uri;->getScheme()Ljava/lang/String;

    move-result-object v0

    const-string v1, "file"

    .line 276
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 277
    invoke-virtual {p1}, Landroid/net/Uri;->getLastPathSegment()Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_0
    const-string v1, "content"

    .line 278
    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_4

    const-string v0, "_display_name"

    .line 279
    filled-new-array {v0}, [Ljava/lang/String;

    move-result-object v4

    .line 284
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v2

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    move-object v3, p1

    invoke-virtual/range {v2 .. v7}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    if-eqz p1, :cond_2

    .line 285
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->getCount()I

    move-result v0

    if-eqz v0, :cond_2

    .line 286
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    const-string v0, "_display_name"

    .line 287
    invoke-interface {p1, v0}, Landroid/database/Cursor;->getColumnIndexOrThrow(Ljava/lang/String;)I

    move-result v0

    invoke-interface {p1, v0}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v0, :cond_2

    if-eqz p1, :cond_1

    .line 295
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_1
    return-object v0

    :catchall_0
    move-exception v0

    goto :goto_0

    :cond_2
    if-eqz p1, :cond_4

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_1

    :catchall_1
    move-exception v0

    move-object p1, v1

    :goto_0
    if-eqz p1, :cond_3

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    .line 297
    :cond_3
    throw v0

    :cond_4
    :goto_1
    return-object v1
.end method


# virtual methods
.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 2

    const/16 v0, 0x1389

    if-ne p1, v0, :cond_0

    const/4 v0, -0x1

    if-ne p2, v0, :cond_0

    if-eqz p3, :cond_0

    .line 267
    invoke-virtual {p3}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachment:Landroid/net/Uri;

    .line 268
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachmentView:Landroid/widget/TextView;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachment:Landroid/net/Uri;

    invoke-direct {p0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getFilename(Landroid/net/Uri;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 269
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->removeAttachmentView:Landroid/view/View;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 271
    :cond_0
    invoke-super {p0, p1, p2, p3}, Landroid/app/Fragment;->onActivityResult(IILandroid/content/Intent;)V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 17

    move-object/from16 v8, p0

    .line 54
    sget v0, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_fragment_support_form:I

    const/4 v9, 0x0

    move-object/from16 v1, p1

    move-object/from16 v2, p2

    invoke-virtual {v1, v0, v2, v9}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v10

    .line 57
    invoke-virtual/range {p2 .. p2}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    iput-object v0, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    .line 59
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    instance-of v0, v0, Lnet/gogame/gowrap/ui/UIContext;

    if-eqz v0, :cond_0

    .line 60
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/ui/UIContext;

    iput-object v0, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->uiContext:Lnet/gogame/gowrap/ui/UIContext;

    .line 63
    :cond_0
    sget v0, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_back_button:I

    invoke-virtual {v10, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 64
    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;

    invoke-direct {v1, v8}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 75
    sget-object v0, Lnet/gogame/gowrap/integrations/core/CoreSupport;->INSTANCE:Lnet/gogame/gowrap/integrations/core/CoreSupport;

    invoke-virtual {v0}, Lnet/gogame/gowrap/integrations/core/CoreSupport;->getAppId()Ljava/lang/String;

    move-result-object v0

    const-string v1, "DisneyCrossyRoad-PH-Globe"

    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    .line 77
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_group_mobile_number:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    if-eqz v0, :cond_1

    .line 80
    invoke-virtual {v1, v9}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    :cond_1
    const/16 v2, 0x8

    .line 82
    invoke-virtual {v1, v2}, Landroid/view/View;->setVisibility(I)V

    .line 85
    :goto_0
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_name:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v6, v1

    check-cast v6, Landroid/widget/EditText;

    .line 87
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_email:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v7, v1

    check-cast v7, Landroid/widget/EditText;

    .line 89
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_mobile_number:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v4, v1

    check-cast v4, Landroid/widget/EditText;

    .line 91
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_category:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v11, v1

    check-cast v11, Landroid/widget/TextView;

    .line 93
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_body:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v12, v1

    check-cast v12, Landroid/widget/EditText;

    .line 95
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_attachment:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    iput-object v1, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachmentView:Landroid/widget/TextView;

    .line 97
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_field_remove_attachment:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    iput-object v1, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->removeAttachmentView:Landroid/view/View;

    .line 100
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_support_form_button_send:I

    invoke-virtual {v10, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v13

    .line 102
    iget-object v1, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    const-string v2, "saved_name"

    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/PreferenceUtils;->getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 104
    iget-object v2, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    const-string v3, "saved_email"

    invoke-static {v2, v3}, Lnet/gogame/gowrap/support/PreferenceUtils;->getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 106
    iget-object v3, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    const-string v5, "saved_mobile_number"

    invoke-static {v3, v5}, Lnet/gogame/gowrap/support/PreferenceUtils;->getPreference(Landroid/content/Context;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    if-eqz v1, :cond_2

    .line 109
    invoke-virtual {v6, v1}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    :cond_2
    if-eqz v2, :cond_3

    .line 112
    invoke-virtual {v7, v2}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    :cond_3
    if-eqz v3, :cond_4

    if-eqz v0, :cond_4

    .line 116
    invoke-virtual {v4, v3}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    .line 120
    :cond_4
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 121
    invoke-static {}, Lnet/gogame/gowrap/support/SupportManager;->getCategories()Ljava/util/List;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_1
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lnet/gogame/gowrap/support/SupportCategory;

    .line 122
    new-instance v3, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;

    iget-object v5, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    .line 123
    invoke-virtual {v2}, Lnet/gogame/gowrap/support/SupportCategory;->getStringResourceId()I

    move-result v14

    invoke-virtual {v5, v14}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v5

    invoke-direct {v3, v2, v5}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportCategoryWrapper;-><init>(Lnet/gogame/gowrap/support/SupportCategory;Ljava/lang/String;)V

    .line 122
    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 125
    :cond_5
    new-instance v14, Landroid/widget/ArrayAdapter;

    iget-object v1, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->context:Landroid/content/Context;

    sget v2, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_default_listview_item:I

    sget v3, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_text_view:I

    invoke-direct {v14, v1, v2, v3, v0}, Landroid/widget/ArrayAdapter;-><init>(Landroid/content/Context;IILjava/util/List;)V

    .line 129
    new-instance v15, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;

    move-object v0, v15

    move-object/from16 v1, p0

    move-object v2, v6

    move-object v3, v7

    move-object v5, v12

    invoke-direct/range {v0 .. v5}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$2;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/widget/EditText;Landroid/widget/EditText;Landroid/widget/EditText;Landroid/widget/EditText;)V

    .line 143
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;

    invoke-direct {v0, v8, v15, v13}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$3;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;)V

    .line 161
    invoke-virtual {v6, v0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 162
    invoke-virtual {v7, v0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 163
    invoke-virtual {v12, v0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 165
    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->getLayoutInflater()Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lnet/gogame/gowrap/R$layout;->net_gogame_gowrap_default_listview:I

    const/4 v2, 0x0

    .line 166
    invoke-virtual {v0, v1, v2, v9}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object v0

    .line 167
    sget v1, Lnet/gogame/gowrap/R$id;->net_gogame_gowrap_list_view:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    move-object v12, v1

    check-cast v12, Landroid/widget/ListView;

    .line 169
    invoke-virtual {v12, v14}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    .line 171
    new-instance v7, Landroid/app/Dialog;

    invoke-virtual/range {p0 .. p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    sget v2, Lnet/gogame/gowrap/R$style;->net_gogame_gowrap_dialog:I

    invoke-direct {v7, v1, v2}, Landroid/app/Dialog;-><init>(Landroid/content/Context;I)V

    const/4 v1, 0x1

    .line 172
    invoke-virtual {v7, v1}, Landroid/app/Dialog;->setCanceledOnTouchOutside(Z)V

    .line 173
    invoke-virtual {v7, v0}, Landroid/app/Dialog;->setContentView(Landroid/view/View;)V

    .line 175
    new-instance v6, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;

    move-object v0, v6

    move-object/from16 v1, p0

    move-object v2, v12

    move-object v3, v14

    move-object v4, v11

    move-object v5, v15

    move-object v14, v6

    move-object v6, v13

    move-object/from16 v16, v7

    invoke-direct/range {v0 .. v7}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$4;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/widget/ListView;Landroid/widget/ArrayAdapter;Landroid/widget/TextView;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;Landroid/app/Dialog;)V

    invoke-virtual {v12, v14}, Landroid/widget/ListView;->setOnItemClickListener(Landroid/widget/AdapterView$OnItemClickListener;)V

    .line 197
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;

    move-object/from16 v1, v16

    invoke-direct {v0, v8, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$5;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Landroid/app/Dialog;)V

    invoke-virtual {v11, v0}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 205
    iget-object v0, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->attachmentView:Landroid/widget/TextView;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;

    invoke-direct {v1, v8}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$6;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 219
    iget-object v0, v8, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->removeAttachmentView:Landroid/view/View;

    new-instance v1, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;

    invoke-direct {v1, v8}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$7;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 230
    invoke-virtual {v13, v9}, Landroid/view/View;->setSelected(Z)V

    .line 231
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;

    invoke-direct {v0, v8, v15, v13}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;)V

    invoke-virtual {v13, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-object v10
.end method
