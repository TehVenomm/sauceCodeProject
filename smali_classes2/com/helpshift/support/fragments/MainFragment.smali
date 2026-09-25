.class public abstract Lcom/helpshift/support/fragments/MainFragment;
.super Landroidx/fragment/app/Fragment;
.source "MainFragment.java"


# static fields
.field private static final TAG:Ljava/lang/String;

.field public static final TOOLBAR_ID:Ljava/lang/String; = "toolbarId"

.field private static shouldRetainChildFragmentManager:Z


# instance fields
.field protected fragmentName:Ljava/lang/String;

.field private isChangingConfigurations:Z

.field private isScreenLarge:Z

.field private retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 25
    const-class v0, Lcom/helpshift/support/fragments/SupportFragment;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/helpshift/support/fragments/MainFragment;->TAG:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 22
    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    .line 27
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/MainFragment;->fragmentName:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method protected copyToClipboard(Ljava/lang/String;)V
    .locals 2

    .line 187
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, "clipboard"

    invoke-virtual {v0, v1}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/ClipboardManager;

    const-string v1, "Copy Text"

    .line 188
    invoke-static {v1, p1}, Landroid/content/ClipData;->newPlainText(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Landroid/content/ClipData;

    move-result-object p1

    .line 189
    invoke-virtual {v0, p1}, Landroid/content/ClipboardManager;->setPrimaryClip(Landroid/content/ClipData;)V

    .line 190
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getContext()Landroid/content/Context;

    move-result-object p1

    sget v0, Lcom/helpshift/R$string;->hs__copied_to_clipboard:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/MainFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    const/4 v1, 0x0

    invoke-static {p1, v0, v1}, Lcom/helpshift/views/HSToast;->makeText(Landroid/content/Context;Ljava/lang/CharSequence;I)Landroid/widget/Toast;

    move-result-object p1

    .line 191
    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    return-void
.end method

.method public getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;
    .locals 1

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 166
    :cond_0
    :goto_0
    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 167
    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    goto :goto_0

    .line 169
    :cond_1
    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    return-object p1
.end method

.method public getContext()Landroid/content/Context;
    .locals 1

    .line 59
    invoke-super {p0}, Landroidx/fragment/app/Fragment;->getContext()Landroid/content/Context;

    move-result-object v0

    if-eqz v0, :cond_0

    return-object v0

    .line 64
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    return-object v0
.end method

.method public getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;
    .locals 1

    .line 44
    sget-boolean v0, Lcom/helpshift/support/fragments/MainFragment;->shouldRetainChildFragmentManager:Z

    if-eqz v0, :cond_1

    .line 45
    iget-object v0, p0, Lcom/helpshift/support/fragments/MainFragment;->retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;

    if-nez v0, :cond_0

    .line 46
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/MainFragment;->retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;

    .line 48
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/MainFragment;->retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;

    return-object v0

    .line 50
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    return-object v0
.end method

.method public isChangingConfigurations()Z
    .locals 1

    .line 54
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/MainFragment;->isChangingConfigurations:Z

    return v0
.end method

.method public isScreenLarge()Z
    .locals 1

    .line 175
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/MainFragment;->isScreenLarge:Z

    return v0
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 2

    .line 70
    invoke-static {p1}, Lcom/helpshift/util/ApplicationUtil;->getContextWithUpdatedLocaleLegacy(Landroid/content/Context;)Landroid/content/Context;

    move-result-object v0

    invoke-super {p0, v0}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/content/Context;)V

    const/4 v0, 0x1

    .line 78
    :try_start_0
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/MainFragment;->setRetainInstance(Z)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 81
    :catch_0
    sput-boolean v0, Lcom/helpshift/support/fragments/MainFragment;->shouldRetainChildFragmentManager:Z

    .line 85
    :goto_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    if-nez v1, :cond_0

    .line 86
    invoke-virtual {p1}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/util/HelpshiftContext;->setApplicationContext(Landroid/content/Context;)V

    .line 89
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/util/Styles;->isTablet(Landroid/content/Context;)Z

    move-result p1

    iput-boolean p1, p0, Lcom/helpshift/support/fragments/MainFragment;->isScreenLarge:Z

    .line 93
    sget-boolean p1, Lcom/helpshift/support/fragments/MainFragment;->shouldRetainChildFragmentManager:Z

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/helpshift/support/fragments/MainFragment;->retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;

    if-eqz p1, :cond_1

    .line 95
    :try_start_1
    const-class p1, Landroidx/fragment/app/Fragment;

    const-string v1, "mChildFragmentManager"

    invoke-virtual {p1, v1}, Ljava/lang/Class;->getDeclaredField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object p1

    .line 96
    invoke-virtual {p1, v0}, Ljava/lang/reflect/Field;->setAccessible(Z)V

    .line 97
    iget-object v0, p0, Lcom/helpshift/support/fragments/MainFragment;->retainedChildFragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {p1, p0, v0}, Ljava/lang/reflect/Field;->set(Ljava/lang/Object;Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/lang/NoSuchFieldException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/IllegalAccessException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_1
    move-exception p1

    .line 103
    sget-object v0, Lcom/helpshift/support/fragments/MainFragment;->TAG:Ljava/lang/String;

    const-string v1, "IllegalAccessException"

    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_1

    :catch_2
    move-exception p1

    .line 100
    sget-object v0, Lcom/helpshift/support/fragments/MainFragment;->TAG:Ljava/lang/String;

    const-string v1, "NoSuchFieldException"

    invoke-static {v0, v1, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_1
    :goto_1
    return-void
.end method

.method public onCreateAnimation(IZI)Landroid/view/animation/Animation;
    .locals 1

    .line 115
    invoke-static {}, Lcom/helpshift/model/InfoModelFactory;->getInstance()Lcom/helpshift/model/InfoModelFactory;

    move-result-object v0

    iget-object v0, v0, Lcom/helpshift/model/InfoModelFactory;->appInfoModel:Lcom/helpshift/model/AppInfoModel;

    iget-object v0, v0, Lcom/helpshift/model/AppInfoModel;->disableAnimations:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_0

    if-nez p2, :cond_0

    .line 116
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->isRemoving()Z

    move-result v0

    if-nez v0, :cond_0

    .line 117
    new-instance p1, Landroid/view/animation/AlphaAnimation;

    const/high16 p2, 0x3f800000    # 1.0f

    invoke-direct {p1, p2, p2}, Landroid/view/animation/AlphaAnimation;-><init>(FF)V

    .line 118
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget p3, Lcom/helpshift/R$integer;->hs_animation_duration:I

    invoke-virtual {p2, p3}, Landroid/content/res/Resources;->getInteger(I)I

    move-result p2

    int-to-long p2, p2

    invoke-virtual {p1, p2, p3}, Landroid/view/animation/Animation;->setDuration(J)V

    return-object p1

    .line 121
    :cond_0
    invoke-super {p0, p1, p2, p3}, Landroidx/fragment/app/Fragment;->onCreateAnimation(IZI)Landroid/view/animation/Animation;

    move-result-object p1

    return-object p1
.end method

.method public onPause()V
    .locals 1

    .line 137
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/MainFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->isChangingConfigurations()Z

    move-result v0

    iput-boolean v0, p0, Lcom/helpshift/support/fragments/MainFragment;->isChangingConfigurations:Z

    .line 138
    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onPause()V

    return-void
.end method

.method public onStart()V
    .locals 2

    .line 126
    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    .line 127
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->shouldRefreshMenu()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 128
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 130
    iget-object v1, p0, Lcom/helpshift/support/fragments/MainFragment;->fragmentName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/SupportFragment;->addVisibleFragment(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onStop()V
    .locals 2

    .line 143
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/MainFragment;->shouldRefreshMenu()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 144
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 146
    iget-object v1, p0, Lcom/helpshift/support/fragments/MainFragment;->fragmentName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/fragments/SupportFragment;->removeVisibleFragment(Ljava/lang/String;)V

    .line 149
    :cond_0
    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    return-void
.end method

.method public setToolbarTitle(Ljava/lang/String;)V
    .locals 1

    .line 179
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 181
    invoke-virtual {v0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->setTitle(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public abstract shouldRefreshMenu()Z
.end method
