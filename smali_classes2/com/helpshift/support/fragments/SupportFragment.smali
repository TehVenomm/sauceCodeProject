.class public Lcom/helpshift/support/fragments/SupportFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SupportFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;
.implements Lcom/helpshift/support/contracts/SupportScreenView;
.implements Lcom/helpshift/common/FetchDataFromThread;
.implements Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;
.implements Landroid/view/MenuItem$OnMenuItemClickListener;
.implements Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/fragments/SupportFragment$SupportModes;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/helpshift/support/fragments/MainFragment;",
        "Landroid/view/View$OnClickListener;",
        "Lcom/helpshift/support/contracts/SupportScreenView;",
        "Lcom/helpshift/common/FetchDataFromThread<",
        "Ljava/lang/Integer;",
        "Ljava/lang/Integer;",
        ">;",
        "Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;",
        "Landroid/view/MenuItem$OnMenuItemClickListener;",
        "Lcom/helpshift/support/fragments/IToolbarMenuItemRenderer;"
    }
.end annotation


# static fields
.field public static final SUPPORT_MODE:Ljava/lang/String; = "support_mode"

.field private static final TAG:Ljava/lang/String; = "Helpshift_SupportFrag"


# instance fields
.field private attachImageMenuItem:Landroid/view/MenuItem;

.field contactUsMenuItem:Landroid/view/MenuItem;

.field private doneMenuItem:Landroid/view/MenuItem;

.field private faqLoaded:Z

.field private fragmentMenuItems:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation
.end field

.field private handleNewIntent:Z

.field private imagePicker:Lcom/helpshift/support/widget/ImagePicker;

.field private isForeground:Z

.field private menuItemEventListener:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "Lcom/helpshift/support/fragments/IMenuItemEventListener;",
            ">;"
        }
    .end annotation
.end field

.field private menuItemsPrepared:Z

.field private newIntentData:Landroid/os/Bundle;

.field private newMessageCount:I

.field private searchMenuItem:Landroid/view/MenuItem;

.field private searchView:Landroidx/appcompat/widget/SearchView;

.field private startNewConversationMenuItem:Landroid/view/MenuItem;

.field private supportController:Lcom/helpshift/support/controllers/SupportController;

.field private toolbar:Landroidx/appcompat/widget/Toolbar;

.field private toolbarId:I

.field private toolbarImportanceForAccessibility:I

.field private viewFaqsLoadError:Landroid/view/View;

.field private viewFaqsLoading:Landroid/view/View;

.field private viewNoFaqs:Landroid/view/View;

.field private final visibleFragments:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 84
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    .line 93
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-static {v0}, Ljava/util/Collections;->synchronizedList(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    const/4 v0, 0x0

    .line 106
    iput v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->newMessageCount:I

    return-void
.end method

.method private attachMenuListeners(Landroid/view/Menu;)V
    .locals 2

    .line 207
    sget v0, Lcom/helpshift/R$id;->hs__search:I

    invoke-interface {p1, v0}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    .line 208
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->getActionView(Landroid/view/MenuItem;)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/SearchView;

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchView:Landroidx/appcompat/widget/SearchView;

    .line 210
    sget v0, Lcom/helpshift/R$id;->hs__contact_us:I

    invoke-interface {p1, v0}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    .line 211
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    sget v1, Lcom/helpshift/R$string;->hs__contact_us_btn:I

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setTitle(I)Landroid/view/MenuItem;

    .line 212
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, p0}, Landroid/view/MenuItem;->setOnMenuItemClickListener(Landroid/view/MenuItem$OnMenuItemClickListener;)Landroid/view/MenuItem;

    .line 217
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->getActionView(Landroid/view/MenuItem;)Landroid/view/View;

    move-result-object v0

    new-instance v1, Lcom/helpshift/support/fragments/SupportFragment$1;

    invoke-direct {v1, p0}, Lcom/helpshift/support/fragments/SupportFragment$1;-><init>(Lcom/helpshift/support/fragments/SupportFragment;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 224
    sget v0, Lcom/helpshift/R$id;->hs__action_done:I

    invoke-interface {p1, v0}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    .line 225
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, p0}, Landroid/view/MenuItem;->setOnMenuItemClickListener(Landroid/view/MenuItem$OnMenuItemClickListener;)Landroid/view/MenuItem;

    .line 227
    sget v0, Lcom/helpshift/R$id;->hs__start_new_conversation:I

    invoke-interface {p1, v0}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    .line 228
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, p0}, Landroid/view/MenuItem;->setOnMenuItemClickListener(Landroid/view/MenuItem$OnMenuItemClickListener;)Landroid/view/MenuItem;

    .line 230
    sget v0, Lcom/helpshift/R$id;->hs__attach_screenshot:I

    invoke-interface {p1, v0}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    .line 231
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    invoke-interface {p1, p0}, Landroid/view/MenuItem;->setOnMenuItemClickListener(Landroid/view/MenuItem$OnMenuItemClickListener;)Landroid/view/MenuItem;

    const/4 p1, 0x1

    .line 233
    iput-boolean p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemsPrepared:Z

    const/4 p1, 0x0

    .line 234
    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchListeners(Lcom/helpshift/support/controllers/FaqFlowController;)V

    .line 235
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->refreshMenu()V

    return-void
.end method

.method private findToolbarViewInViewHierarchy(I)Landroidx/appcompat/widget/Toolbar;
    .locals 4

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 863
    :cond_0
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v1, p1}, Landroid/app/Activity;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroidx/appcompat/widget/Toolbar;

    if-eqz v1, :cond_1

    return-object v1

    .line 873
    :cond_1
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v1

    const/4 v2, 0x5

    :goto_0
    add-int/lit8 v3, v2, -0x1

    if-lez v2, :cond_3

    if-eqz v1, :cond_3

    .line 876
    invoke-virtual {v1}, Landroidx/fragment/app/Fragment;->getView()Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_2

    .line 879
    invoke-virtual {v2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    check-cast v2, Landroidx/appcompat/widget/Toolbar;

    if-eqz v2, :cond_2

    return-object v2

    .line 886
    :cond_2
    invoke-virtual {v1}, Landroidx/fragment/app/Fragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v1

    move v2, v3

    goto :goto_0

    :cond_3
    return-object v0
.end method

.method private declared-synchronized getImagePicker()Lcom/helpshift/support/widget/ImagePicker;
    .locals 1

    monitor-enter p0

    .line 737
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->imagePicker:Lcom/helpshift/support/widget/ImagePicker;

    if-nez v0, :cond_0

    .line 738
    new-instance v0, Lcom/helpshift/support/widget/ImagePicker;

    invoke-direct {v0, p0}, Lcom/helpshift/support/widget/ImagePicker;-><init>(Landroidx/fragment/app/Fragment;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->imagePicker:Lcom/helpshift/support/widget/ImagePicker;

    .line 740
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->imagePicker:Lcom/helpshift/support/widget/ImagePicker;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception v0

    .line 736
    monitor-exit p0

    throw v0
.end method

.method private getMenuResourceId()I
    .locals 1

    .line 203
    sget v0, Lcom/helpshift/R$menu;->hs__support_fragment:I

    return v0
.end method

.method private hideAllMenuItems()V
    .locals 2

    .line 304
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    .line 305
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    .line 306
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    .line 307
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    .line 308
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    return-void
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;
    .locals 1

    .line 117
    new-instance v0, Lcom/helpshift/support/fragments/SupportFragment;

    invoke-direct {v0}, Lcom/helpshift/support/fragments/SupportFragment;-><init>()V

    .line 118
    invoke-virtual {v0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private quitSupportFragment()V
    .locals 2

    .line 527
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    .line 528
    instance-of v1, v0, Lcom/helpshift/support/activities/ParentActivity;

    if-eqz v1, :cond_0

    .line 529
    invoke-virtual {v0}, Landroid/app/Activity;->finish()V

    goto :goto_0

    .line 532
    :cond_0
    check-cast v0, Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v0}, Landroidx/appcompat/app/AppCompatActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    .line 533
    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :goto_0
    return-void
.end method

.method private restoreConversationFragmentMenu()V
    .locals 3

    const/4 v0, 0x1

    .line 377
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainSearchFragmentState(Z)V

    const/4 v0, 0x0

    .line 378
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 379
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    .line 382
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    const-string v2, "HSNewConversationFragment"

    .line 383
    invoke-virtual {v1, v2}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-nez v1, :cond_0

    .line 386
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    const-string v2, "HSConversationFragment"

    .line 387
    invoke-virtual {v1, v2}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/conversations/BaseConversationFragment;

    :cond_0
    if-eqz v1, :cond_1

    .line 390
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1, v0}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    :cond_1
    return-void
.end method

.method private restoreSearchMenuItem()V
    .locals 1

    .line 421
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getFaqFlowFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/FaqFlowFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 423
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getSearchFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/SearchFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 425
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/SearchFragment;->getCurrentQuery()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuQuery(Ljava/lang/String;)V

    .line 428
    :cond_0
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    const/4 v0, 0x0

    .line 429
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainSearchFragmentState(Z)V

    return-void
.end method

.method private restoreSingleQuestionDoneModeFragmentMenu()V
    .locals 2

    .line 373
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    return-void
.end method

.method private sendMenuEventClickEvent(Lcom/helpshift/support/fragments/HSMenuItemType;)V
    .locals 1

    .line 262
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 263
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/IMenuItemEventListener;

    invoke-interface {v0, p1}, Lcom/helpshift/support/fragments/IMenuItemEventListener;->onMenuItemClicked(Lcom/helpshift/support/fragments/HSMenuItemType;)V

    :cond_0
    return-void
.end method

.method private setMenuItemColors()V
    .locals 3

    .line 291
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    .line 292
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1}, Landroid/view/MenuItem;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 293
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1}, Landroid/view/MenuItem;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 294
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-static {v1}, Lcom/helpshift/views/HSMenuItemCompat;->getActionView(Landroid/view/MenuItem;)Landroid/view/View;

    move-result-object v1

    .line 295
    sget v2, Lcom/helpshift/R$id;->hs__notification_badge:I

    invoke-virtual {v1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 296
    invoke-virtual {v1}, Landroid/widget/TextView;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 297
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->doneMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1}, Landroid/view/MenuItem;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 299
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1}, Landroid/view/MenuItem;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 300
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    invoke-interface {v1}, Landroid/view/MenuItem;->getIcon()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/helpshift/util/Styles;->setActionButtonIconColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method private setRetainSearchFragmentState(Z)V
    .locals 2

    .line 413
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-string v1, "Helpshift_FaqFlowFrag"

    .line 414
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/FaqFlowFragment;

    if-eqz v0, :cond_0

    .line 415
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 416
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/FaqFlowController;->setRetainSearchFragmentState(Z)V

    :cond_0
    return-void
.end method

.method private showDynamicFormFragmentMenu()V
    .locals 1

    const/4 v0, 0x1

    .line 438
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainSearchFragmentState(Z)V

    const/4 v0, 0x0

    .line 439
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    .line 440
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    return-void
.end method

.method private showFaqFragmentMenu()V
    .locals 1

    .line 433
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->faqLoaded:Z

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 434
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    return-void
.end method

.method private showQuestionListFragmentMenu()V
    .locals 1

    .line 395
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->faqLoaded:Z

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 396
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    return-void
.end method

.method private showSectionPagerFragmentMenu()V
    .locals 1

    const/4 v0, 0x1

    .line 400
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 401
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    return-void
.end method

.method private showSingleQuestionFragmentMenu()V
    .locals 1

    .line 405
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->isScreenLarge()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    .line 406
    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainSearchFragmentState(Z)V

    const/4 v0, 0x0

    .line 407
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 409
    :cond_0
    sget-object v0, Lcom/helpshift/support/ContactUsFilter$LOCATION;->QUESTION_ACTION_BAR:Lcom/helpshift/support/ContactUsFilter$LOCATION;

    invoke-static {v0}, Lcom/helpshift/support/ContactUsFilter;->showContactUs(Lcom/helpshift/support/ContactUsFilter$LOCATION;)Z

    move-result v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    return-void
.end method

.method private showToolbarElevationLollipop(Z)V
    .locals 3
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .line 628
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    const/4 v1, 0x0

    const/high16 v2, 0x40800000    # 4.0f

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 630
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v0

    invoke-virtual {p1, v0}, Landroidx/appcompat/widget/Toolbar;->setElevation(F)V

    goto :goto_0

    .line 633
    :cond_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p1, v1}, Landroidx/appcompat/widget/Toolbar;->setElevation(F)V

    goto :goto_0

    .line 637
    :cond_1
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v0}, Landroidx/appcompat/app/AppCompatActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    if-eqz v0, :cond_3

    if-eqz p1, :cond_2

    .line 640
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result p1

    invoke-virtual {v0, p1}, Landroidx/appcompat/app/ActionBar;->setElevation(F)V

    goto :goto_0

    .line 643
    :cond_2
    invoke-virtual {v0, v1}, Landroidx/appcompat/app/ActionBar;->setElevation(F)V

    :cond_3
    :goto_0
    return-void
.end method

.method private showToolbarElevationPreLollipop(Z)V
    .locals 2

    .line 650
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    sget v1, Lcom/helpshift/R$id;->flow_fragment_container:I

    .line 651
    invoke-virtual {v0, v1}, Landroid/app/Activity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/FrameLayout;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_0

    .line 655
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v1, Lcom/helpshift/R$drawable;->hs__actionbar_compat_shadow:I

    invoke-virtual {p1, v1}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object p1

    .line 656
    invoke-virtual {v0, p1}, Landroid/widget/FrameLayout;->setForeground(Landroid/graphics/drawable/Drawable;)V

    goto :goto_0

    .line 659
    :cond_0
    new-instance p1, Landroid/graphics/drawable/ColorDrawable;

    const/4 v1, 0x0

    invoke-direct {p1, v1}, Landroid/graphics/drawable/ColorDrawable;-><init>(I)V

    invoke-virtual {v0, p1}, Landroid/widget/FrameLayout;->setForeground(Landroid/graphics/drawable/Drawable;)V

    :cond_1
    :goto_0
    return-void
.end method

.method private startLiveUpdates()V
    .locals 2

    .line 539
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-string v1, "HSConversationFragment"

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/ConversationalFragment;

    if-eqz v0, :cond_0

    .line 541
    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragment;->startLiveUpdates()V

    :cond_0
    return-void
.end method

.method private stopLiveUpdates()V
    .locals 2

    .line 547
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-string v1, "HSConversationFragment"

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/ConversationalFragment;

    if-eqz v0, :cond_0

    .line 549
    invoke-virtual {v0}, Lcom/helpshift/support/conversations/ConversationalFragment;->stopLiveUpdates()V

    :cond_0
    return-void
.end method

.method private updateBadgeIcon()V
    .locals 5

    .line 472
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0}, Landroid/view/MenuItem;->isVisible()Z

    move-result v0

    if-eqz v0, :cond_1

    .line 473
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->getActionView(Landroid/view/MenuItem;)Landroid/view/View;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 475
    sget v1, Lcom/helpshift/R$id;->hs__notification_badge:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 476
    sget v2, Lcom/helpshift/R$id;->hs__notification_badge_padding:I

    invoke-virtual {v0, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 477
    iget v2, p0, Lcom/helpshift/support/fragments/SupportFragment;->newMessageCount:I

    const/4 v3, 0x0

    const/16 v4, 0x8

    if-eqz v2, :cond_0

    .line 478
    iget v2, p0, Lcom/helpshift/support/fragments/SupportFragment;->newMessageCount:I

    invoke-static {v2}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 479
    invoke-virtual {v0, v4}, Landroid/view/View;->setVisibility(I)V

    .line 480
    invoke-virtual {v1, v3}, Landroid/widget/TextView;->setVisibility(I)V

    goto :goto_0

    .line 483
    :cond_0
    invoke-virtual {v1, v4}, Landroid/widget/TextView;->setVisibility(I)V

    .line 484
    invoke-virtual {v0, v3}, Landroid/view/View;->setVisibility(I)V

    :cond_1
    :goto_0
    return-void
.end method

.method private updateMessageBatchCount(Ljava/lang/Integer;)V
    .locals 0

    .line 701
    invoke-virtual {p1}, Ljava/lang/Integer;->intValue()I

    move-result p1

    iput p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->newMessageCount:I

    .line 702
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->updateBadgeIcon()V

    return-void
.end method


# virtual methods
.method public addVisibleFragment(Ljava/lang/String;)V
    .locals 1

    .line 312
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 313
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->refreshMenu()V

    return-void
.end method

.method public askForReadStoragePermission()V
    .locals 3

    .line 967
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-string v1, "HSConversationFragment"

    .line 968
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-nez v0, :cond_0

    .line 971
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-string v1, "HSNewConversationFragment"

    .line 972
    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/conversations/BaseConversationFragment;

    :cond_0
    if-eqz v0, :cond_1

    const/4 v1, 0x1

    const/4 v2, 0x2

    .line 975
    invoke-virtual {v0, v1, v2}, Lcom/helpshift/support/conversations/BaseConversationFragment;->requestPermission(ZI)V

    :cond_1
    return-void
.end method

.method public exitSdkSession()V
    .locals 1

    .line 666
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    instance-of v0, v0, Lcom/helpshift/support/activities/ParentActivity;

    if-eqz v0, :cond_0

    .line 667
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->finish()V

    goto :goto_0

    .line 670
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/helpshift/support/util/FragmentUtil;->removeFragment(Landroidx/fragment/app/FragmentManager;Landroidx/fragment/app/Fragment;)V

    :goto_0
    return-void
.end method

.method public getSupportController()Lcom/helpshift/support/controllers/SupportController;
    .locals 1

    .line 123
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    return-object v0
.end method

.method public launchImagePicker(ZLandroid/os/Bundle;)V
    .locals 0

    if-eqz p1, :cond_0

    .line 678
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getImagePicker()Lcom/helpshift/support/widget/ImagePicker;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/support/widget/ImagePicker;->checkPermissionAndLaunchImagePicker(Landroid/os/Bundle;)V

    goto :goto_0

    .line 681
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getImagePicker()Lcom/helpshift/support/widget/ImagePicker;

    move-result-object p1

    invoke-virtual {p1, p2}, Lcom/helpshift/support/widget/ImagePicker;->launchImagePicker(Landroid/os/Bundle;)V

    :goto_0
    return-void
.end method

.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 1

    .line 760
    invoke-super {p0, p1, p2, p3}, Lcom/helpshift/support/fragments/MainFragment;->onActivityResult(IILandroid/content/Intent;)V

    const/4 v0, 0x1

    if-eq p1, v0, :cond_0

    const/4 v0, 0x2

    if-ne p1, v0, :cond_1

    :cond_0
    if-eqz p3, :cond_1

    const/4 v0, -0x1

    if-ne p2, v0, :cond_1

    .line 763
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getImagePicker()Lcom/helpshift/support/widget/ImagePicker;

    move-result-object p2

    invoke-virtual {p2, p1, p3}, Lcom/helpshift/support/widget/ImagePicker;->onImagePickRequestResult(ILandroid/content/Intent;)V

    :cond_1
    return-void
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 4

    .line 128
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    .line 129
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object p1

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-interface {p1, v0}, Lcom/helpshift/common/platform/Platform;->setUIContext(Ljava/lang/Object;)V

    const/4 p1, 0x1

    .line 130
    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainInstance(Z)V

    .line 131
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    if-nez v0, :cond_0

    .line 132
    new-instance v0, Lcom/helpshift/support/controllers/SupportController;

    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v2

    .line 133
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v3

    invoke-direct {v0, v1, p0, v2, v3}, Lcom/helpshift/support/controllers/SupportController;-><init>(Landroid/content/Context;Lcom/helpshift/support/contracts/SupportScreenView;Landroidx/fragment/app/FragmentManager;Landroid/os/Bundle;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    goto :goto_0

    .line 136
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/support/controllers/SupportController;->onFragmentManagerUpdate(Landroidx/fragment/app/FragmentManager;)V

    .line 138
    :goto_0
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_1

    .line 139
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V

    :cond_1
    return-void
.end method

.method public onBackPressed()Z
    .locals 6

    .line 572
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_5

    .line 574
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroidx/fragment/app/Fragment;

    if-eqz v2, :cond_0

    .line 575
    invoke-virtual {v2}, Landroidx/fragment/app/Fragment;->isVisible()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 576
    instance-of v3, v2, Lcom/helpshift/support/fragments/FaqFlowFragment;

    if-nez v3, :cond_2

    instance-of v3, v2, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-eqz v3, :cond_1

    goto :goto_0

    .line 595
    :cond_1
    instance-of v3, v2, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    if-eqz v3, :cond_0

    .line 596
    check-cast v2, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;

    invoke-virtual {v2}, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment;->deleteAttachmentLocalCopy()V

    return v1

    .line 577
    :cond_2
    :goto_0
    invoke-virtual {v2}, Landroidx/fragment/app/Fragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v3

    .line 578
    invoke-virtual {v3}, Landroidx/fragment/app/FragmentManager;->getBackStackEntryCount()I

    move-result v4

    const/4 v5, 0x1

    if-lez v4, :cond_3

    .line 579
    invoke-virtual {v3}, Landroidx/fragment/app/FragmentManager;->popBackStack()V

    return v5

    .line 583
    :cond_3
    instance-of v3, v2, Lcom/helpshift/support/conversations/ConversationalFragment;

    if-eqz v3, :cond_0

    .line 584
    check-cast v2, Lcom/helpshift/support/conversations/ConversationalFragment;

    .line 585
    invoke-virtual {v2}, Lcom/helpshift/support/conversations/ConversationalFragment;->onBackPressed()Z

    move-result v0

    if-eqz v0, :cond_4

    return v5

    .line 589
    :cond_4
    invoke-virtual {v2}, Lcom/helpshift/support/conversations/ConversationalFragment;->stopLiveUpdates()V

    return v1

    :cond_5
    return v1
.end method

.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 728
    invoke-virtual {p1}, Landroid/view/View;->getId()I

    move-result p1

    sget v0, Lcom/helpshift/R$id;->button_retry:I

    if-ne p1, v0, :cond_0

    .line 729
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/util/FragmentUtil;->getFaqFlowFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/FaqFlowFragment;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 731
    invoke-virtual {p1}, Lcom/helpshift/support/fragments/FaqFlowFragment;->retryGetSections()V

    :cond_0
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 1

    .line 785
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onCreate(Landroid/os/Bundle;)V

    .line 788
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_0

    const-string v0, "toolbarId"

    .line 790
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result p1

    iput p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarId:I

    .line 794
    :cond_0
    iget p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarId:I

    if-nez p1, :cond_1

    const/4 p1, 0x1

    .line 795
    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->setHasOptionsMenu(Z)V

    :cond_1
    return-void
.end method

.method public onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V
    .locals 1

    .line 956
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getMenuResourceId()I

    move-result v0

    invoke-virtual {p2, v0, p1}, Landroid/view/MenuInflater;->inflate(ILandroid/view/Menu;)V

    .line 957
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->attachMenuListeners(Landroid/view/Menu;)V

    .line 958
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 959
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/fragments/IMenuItemEventListener;

    invoke-interface {v0}, Lcom/helpshift/support/fragments/IMenuItemEventListener;->onCreateOptionMenuCalled()V

    .line 961
    :cond_0
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 802
    sget p3, Lcom/helpshift/R$layout;->hs__support_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDataFetched(Ljava/lang/Integer;)V
    .locals 0

    .line 707
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->updateMessageBatchCount(Ljava/lang/Integer;)V

    return-void
.end method

.method public bridge synthetic onDataFetched(Ljava/lang/Object;)V
    .locals 0

    .line 84
    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->onDataFetched(Ljava/lang/Integer;)V

    return-void
.end method

.method public onDestroyView()V
    .locals 3

    .line 927
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getView()Landroid/view/View;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/SnackbarUtil;->hideSnackbar(Landroid/view/View;)V

    .line 929
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->fragmentMenuItems:Ljava/util/List;

    if-eqz v0, :cond_0

    .line 930
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0}, Landroidx/appcompat/widget/Toolbar;->getMenu()Landroid/view/Menu;

    move-result-object v0

    .line 931
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->fragmentMenuItems:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Integer;

    .line 932
    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    invoke-interface {v0, v2}, Landroid/view/Menu;->removeItem(I)V

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 937
    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoadError:Landroid/view/View;

    .line 938
    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoading:Landroid/view/View;

    .line 939
    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewNoFaqs:Landroid/view/View;

    .line 941
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onDetach()V
    .locals 2

    .line 946
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/Platform;->setUIContext(Ljava/lang/Object;)V

    .line 947
    invoke-static {}, Lcom/helpshift/util/ApplicationUtil;->restoreApplicationLocale()V

    .line 948
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    .line 949
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationInboxPoller()Lcom/helpshift/conversation/ConversationInboxPoller;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/conversation/ConversationInboxPoller;->startAppPoller(Z)V

    .line 951
    :cond_0
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDetach()V

    return-void
.end method

.method public onFailure(Ljava/lang/Integer;)V
    .locals 0

    return-void
.end method

.method public bridge synthetic onFailure(Ljava/lang/Object;)V
    .locals 0

    .line 84
    check-cast p1, Ljava/lang/Integer;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->onFailure(Ljava/lang/Integer;)V

    return-void
.end method

.method public onFaqsLoaded()V
    .locals 3

    const/4 v0, 0x1

    .line 321
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->faqLoaded:Z

    .line 322
    iget-boolean v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemsPrepared:Z

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    const-class v2, Lcom/helpshift/support/compositions/FaqFragment;

    .line 323
    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-nez v1, :cond_0

    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    const-class v2, Lcom/helpshift/support/fragments/QuestionListFragment;

    .line 324
    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v1, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 325
    :cond_0
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    :cond_1
    return-void
.end method

.method public onFocusChanged(Z)V
    .locals 3

    .line 561
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 563
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/fragment/app/Fragment;

    .line 564
    instance-of v2, v1, Lcom/helpshift/support/conversations/ConversationalFragment;

    if-eqz v2, :cond_0

    .line 565
    check-cast v1, Lcom/helpshift/support/conversations/ConversationalFragment;

    invoke-virtual {v1, p1}, Lcom/helpshift/support/conversations/ConversationalFragment;->onFocusChanged(Z)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public onImagePickerResultFailure(ILjava/lang/Long;)V
    .locals 5

    const/4 v0, -0x1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 997
    :pswitch_0
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__screenshot_cloud_attach_error:I

    invoke-static {p1, p2, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;II)V

    goto :goto_0

    .line 992
    :pswitch_1
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__screenshot_upload_error_msg:I

    invoke-static {p1, p2, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;II)V

    goto :goto_0

    .line 1002
    :pswitch_2
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v1, Lcom/helpshift/R$string;->hs__screenshot_limit_error:I

    invoke-virtual {p1, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    const/4 v1, 0x1

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    .line 1003
    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    long-to-float p2, v3

    const/high16 v3, 0x49800000    # 1048576.0f

    div-float/2addr p2, v3

    invoke-static {p2}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object p2

    aput-object p2, v1, v2

    .line 1002
    invoke-static {p1, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    .line 1004
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getView()Landroid/view/View;

    move-result-object p2

    invoke-static {p2, p1, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;Ljava/lang/CharSequence;I)V

    goto :goto_0

    .line 989
    :pswitch_3
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getView()Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/helpshift/R$string;->hs__network_error_msg:I

    invoke-static {p1, p2, v0}, Lcom/helpshift/support/util/SnackbarUtil;->showSnackbar(Landroid/view/View;II)V

    :goto_0
    return-void

    nop

    :pswitch_data_0
    .packed-switch -0x4
        :pswitch_3
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public onImagePickerResultSuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;)V
    .locals 2

    .line 982
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getSupportController()Lcom/helpshift/support/controllers/SupportController;

    move-result-object v0

    sget-object v1, Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;->GALLERY_APP:Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;

    invoke-virtual {v0, p1, p2, v1}, Lcom/helpshift/support/controllers/SupportController;->startScreenshotPreviewFragment(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;Lcom/helpshift/support/fragments/ScreenshotPreviewFragment$LaunchSource;)V

    return-void
.end method

.method public onMenuItemClick(Landroid/view/MenuItem;)Z
    .locals 2

    .line 240
    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result p1

    .line 242
    sget v0, Lcom/helpshift/R$id;->hs__contact_us:I

    const/4 v1, 0x1

    if-ne p1, v0, :cond_0

    .line 243
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Lcom/helpshift/support/controllers/SupportController;->onContactUsClicked(Ljava/lang/String;)V

    goto :goto_0

    .line 246
    :cond_0
    sget v0, Lcom/helpshift/R$id;->hs__action_done:I

    if-ne p1, v0, :cond_1

    .line 247
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {p1}, Lcom/helpshift/support/controllers/SupportController;->actionDone()V

    goto :goto_0

    .line 250
    :cond_1
    sget v0, Lcom/helpshift/R$id;->hs__start_new_conversation:I

    if-ne p1, v0, :cond_2

    .line 251
    sget-object p1, Lcom/helpshift/support/fragments/HSMenuItemType;->START_NEW_CONVERSATION:Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->sendMenuEventClickEvent(Lcom/helpshift/support/fragments/HSMenuItemType;)V

    goto :goto_0

    .line 254
    :cond_2
    sget v0, Lcom/helpshift/R$id;->hs__attach_screenshot:I

    if-ne p1, v0, :cond_3

    .line 255
    sget-object p1, Lcom/helpshift/support/fragments/HSMenuItemType;->SCREENSHOT_ATTACHMENT:Lcom/helpshift/support/fragments/HSMenuItemType;

    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->sendMenuEventClickEvent(Lcom/helpshift/support/fragments/HSMenuItemType;)V

    goto :goto_0

    :cond_3
    const/4 v1, 0x0

    :goto_0
    return v1
.end method

.method public onNewIntent(Landroid/os/Bundle;)V
    .locals 1

    .line 691
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->isForeground:Z

    if-eqz v0, :cond_0

    .line 692
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/SupportController;->onNewIntent(Landroid/os/Bundle;)V

    goto :goto_0

    .line 695
    :cond_0
    iput-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->newIntentData:Landroid/os/Bundle;

    .line 697
    :goto_0
    iget-boolean p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->isForeground:Z

    xor-int/lit8 p1, p1, 0x1

    iput-boolean p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->handleNewIntent:Z

    return-void
.end method

.method public onPause()V
    .locals 1

    .line 176
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/Activity;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    .line 177
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->stopLiveUpdates()V

    .line 179
    :cond_0
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onPause()V

    return-void
.end method

.method public onRequestPermissionsResult(I[Ljava/lang/String;[I)V
    .locals 3

    .line 769
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 771
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/fragment/app/Fragment;

    if-eqz v1, :cond_0

    .line 773
    invoke-virtual {v1}, Landroidx/fragment/app/Fragment;->isVisible()Z

    move-result v2

    if-eqz v2, :cond_0

    instance-of v2, v1, Lcom/helpshift/support/conversations/BaseConversationFragment;

    if-eqz v2, :cond_0

    .line 775
    invoke-virtual {v1, p1, p2, p3}, Landroidx/fragment/app/Fragment;->onRequestPermissionsResult(I[Ljava/lang/String;[I)V

    return-void

    .line 780
    :cond_1
    invoke-super {p0, p1, p2, p3}, Lcom/helpshift/support/fragments/MainFragment;->onRequestPermissionsResult(I[Ljava/lang/String;[I)V

    return-void
.end method

.method public onResume()V
    .locals 2

    .line 905
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    .line 906
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0}, Lcom/helpshift/support/controllers/SupportController;->start()V

    .line 907
    sget v0, Lcom/helpshift/R$string;->hs__help_header:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->getString(I)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->setToolbarTitle(Ljava/lang/String;)V

    const/4 v0, 0x1

    .line 908
    invoke-virtual {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->showToolbarElevation(Z)V

    .line 909
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    new-instance v1, Ljava/util/concurrent/atomic/AtomicReference;

    invoke-direct {v1, p0}, Ljava/util/concurrent/atomic/AtomicReference;-><init>(Ljava/lang/Object;)V

    iput-object v1, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;

    .line 911
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->startLiveUpdates()V

    .line 913
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getNotificationCountSync()I

    move-result v0

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->updateMessageBatchCount(Ljava/lang/Integer;)V

    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 1

    .line 918
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    .line 919
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    if-eqz v0, :cond_0

    .line 920
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/SupportController;->onSaveInstanceState(Landroid/os/Bundle;)V

    .line 922
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getImagePicker()Lcom/helpshift/support/widget/ImagePicker;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/support/widget/ImagePicker;->onSaveInstanceState(Landroid/os/Bundle;)V

    return-void
.end method

.method public onStart()V
    .locals 3

    .line 145
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStart()V

    .line 147
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    if-nez v0, :cond_0

    .line 148
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->quitSupportFragment()V

    return-void

    .line 152
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_3

    const-string v0, "Helpshift_SupportFrag"

    const-string v1, "Helpshift session began."

    .line 153
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 154
    invoke-static {}, Lcom/helpshift/support/HSSearch;->init()V

    .line 155
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "support_mode"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    if-nez v0, :cond_1

    .line 158
    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED:Lcom/helpshift/analytics/AnalyticsEventType;

    goto :goto_0

    .line 161
    :cond_1
    sget-object v0, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_OPENED_DECOMP:Lcom/helpshift/analytics/AnalyticsEventType;

    .line 163
    :goto_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v1

    invoke-interface {v1}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v1

    invoke-virtual {v1, v0}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    .line 165
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->handleNewIntent:Z

    if-eqz v0, :cond_2

    .line 166
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->newIntentData:Landroid/os/Bundle;

    invoke-virtual {v0, v1}, Lcom/helpshift/support/controllers/SupportController;->onNewIntent(Landroid/os/Bundle;)V

    .line 167
    iput-boolean v2, p0, Lcom/helpshift/support/fragments/SupportFragment;->handleNewIntent:Z

    .line 169
    :cond_2
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->onSDKSessionStarted()V

    :cond_3
    const/4 v0, 0x1

    .line 171
    iput-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->isForeground:Z

    return-void
.end method

.method public onStop()V
    .locals 3

    .line 184
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "Helpshift_SupportFrag"

    const-string v1, "Helpshift session ended."

    .line 185
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 186
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    .line 187
    invoke-static {}, Lcom/helpshift/support/HSSearch;->deinit()V

    .line 188
    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getAnalyticsEventDM()Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/analytics/AnalyticsEventType;->LIBRARY_QUIT:Lcom/helpshift/analytics/AnalyticsEventType;

    invoke-virtual {v1, v2}, Lcom/helpshift/analytics/domainmodel/AnalyticsEventDM;->pushEvent(Lcom/helpshift/analytics/AnalyticsEventType;)V

    const/4 v1, 0x0

    .line 189
    iput-boolean v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->isForeground:Z

    .line 190
    invoke-interface {v0}, Lcom/helpshift/CoreApi;->sendAnalyticsEvent()V

    .line 191
    invoke-interface {v0}, Lcom/helpshift/CoreApi;->onSDKSessionEnded()V

    .line 193
    :cond_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getConversationController()Lcom/helpshift/conversation/domainmodel/ConversationController;

    move-result-object v0

    const/4 v1, 0x0

    iput-object v1, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesListenerReference:Ljava/util/concurrent/atomic/AtomicReference;

    .line 194
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 3

    .line 807
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 808
    sget p2, Lcom/helpshift/R$id;->view_no_faqs:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewNoFaqs:Landroid/view/View;

    .line 809
    sget p2, Lcom/helpshift/R$id;->view_faqs_loading:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoading:Landroid/view/View;

    .line 810
    sget p2, Lcom/helpshift/R$id;->view_faqs_load_error:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoadError:Landroid/view/View;

    .line 811
    sget p2, Lcom/helpshift/R$id;->button_retry:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/Button;

    .line 812
    invoke-virtual {p2, p0}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 814
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object p2

    invoke-interface {p2}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object p2

    invoke-virtual {p2}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->isHelpshiftBrandingDisabled()Z

    move-result p2

    if-eqz p2, :cond_0

    .line 815
    sget p2, Lcom/helpshift/R$id;->hs_logo:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ImageView;

    const/16 p2, 0x8

    .line 816
    invoke-virtual {p1, p2}, Landroid/widget/ImageView;->setVisibility(I)V

    .line 823
    :cond_0
    iget p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarId:I

    if-eqz p1, :cond_4

    .line 824
    iget p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarId:I

    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->findToolbarViewInViewHierarchy(I)Landroidx/appcompat/widget/Toolbar;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    .line 825
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-nez p1, :cond_1

    const-string p1, "Helpshift_SupportFrag"

    const-string p2, "Unable to retrieve toolbarView from dev provided toolbarId via ApiConfig"

    .line 826
    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_2

    .line 830
    :cond_1
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p1}, Landroidx/appcompat/widget/Toolbar;->getMenu()Landroid/view/Menu;

    move-result-object p1

    .line 831
    new-instance p2, Ljava/util/ArrayList;

    invoke-direct {p2}, Ljava/util/ArrayList;-><init>()V

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 832
    :goto_0
    invoke-interface {p1}, Landroid/view/Menu;->size()I

    move-result v2

    if-ge v1, v2, :cond_2

    .line 833
    invoke-interface {p1, v1}, Landroid/view/Menu;->getItem(I)Landroid/view/MenuItem;

    move-result-object v2

    invoke-interface {v2}, Landroid/view/MenuItem;->getItemId()I

    move-result v2

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {p2, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 836
    :cond_2
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getMenuResourceId()I

    move-result v1

    invoke-virtual {p1, v1}, Landroidx/appcompat/widget/Toolbar;->inflateMenu(I)V

    .line 837
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p1}, Landroidx/appcompat/widget/Toolbar;->getMenu()Landroid/view/Menu;

    move-result-object p1

    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->attachMenuListeners(Landroid/view/Menu;)V

    .line 840
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p1}, Landroidx/appcompat/widget/Toolbar;->getMenu()Landroid/view/Menu;

    move-result-object p1

    .line 841
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    iput-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->fragmentMenuItems:Ljava/util/List;

    .line 842
    :goto_1
    invoke-interface {p1}, Landroid/view/Menu;->size()I

    move-result v1

    if-ge v0, v1, :cond_4

    .line 843
    invoke-interface {p1, v0}, Landroid/view/Menu;->getItem(I)Landroid/view/MenuItem;

    move-result-object v1

    invoke-interface {v1}, Landroid/view/MenuItem;->getItemId()I

    move-result v1

    .line 844
    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {p2, v2}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_3

    .line 845
    iget-object v2, p0, Lcom/helpshift/support/fragments/SupportFragment;->fragmentMenuItems:Ljava/util/List;

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v2, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_3
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    :cond_4
    :goto_2
    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 894
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_1

    .line 896
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    if-eqz v0, :cond_0

    .line 897
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->supportController:Lcom/helpshift/support/controllers/SupportController;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/controllers/SupportController;->onViewStateRestored(Landroid/os/Bundle;)V

    .line 899
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getImagePicker()Lcom/helpshift/support/widget/ImagePicker;

    move-result-object v0

    invoke-virtual {v0, p1}, Lcom/helpshift/support/widget/ImagePicker;->onViewStateRestored(Landroid/os/Bundle;)V

    :cond_1
    return-void
.end method

.method public refreshMenu()V
    .locals 6

    .line 330
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemsPrepared:Z

    if-eqz v0, :cond_c

    .line 331
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->hideAllMenuItems()V

    .line 332
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->setMenuItemColors()V

    .line 333
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    monitor-enter v0

    .line 334
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_0
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_b

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 335
    const-class v3, Lcom/helpshift/support/compositions/FaqFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 336
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->showFaqFragmentMenu()V

    goto :goto_0

    .line 338
    :cond_1
    const-class v3, Lcom/helpshift/support/fragments/SearchFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 339
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->restoreSearchMenuItem()V

    goto :goto_0

    .line 341
    :cond_2
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-class v4, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    invoke-virtual {v4}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/4 v4, 0x1

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 342
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->showSingleQuestionFragmentMenu()V

    goto :goto_0

    .line 344
    :cond_3
    const-class v3, Lcom/helpshift/support/compositions/SectionPagerFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_4

    .line 345
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->showSectionPagerFragmentMenu()V

    goto :goto_0

    .line 347
    :cond_4
    const-class v3, Lcom/helpshift/support/fragments/QuestionListFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_5

    .line 348
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->showQuestionListFragmentMenu()V

    goto :goto_0

    .line 350
    :cond_5
    const-class v3, Lcom/helpshift/support/conversations/NewConversationFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_a

    const-class v3, Lcom/helpshift/support/conversations/ConversationalFragment;

    .line 351
    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_6

    goto :goto_1

    .line 354
    :cond_6
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-class v5, Lcom/helpshift/support/fragments/SingleQuestionFragment;

    invoke-virtual {v5}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/4 v5, 0x2

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_7

    .line 355
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->restoreSingleQuestionDoneModeFragmentMenu()V

    goto/16 :goto_0

    .line 357
    :cond_7
    const-class v3, Lcom/helpshift/support/fragments/DynamicFormFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_8

    .line 358
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->showDynamicFormFragmentMenu()V

    goto/16 :goto_0

    .line 360
    :cond_8
    const-class v3, Lcom/helpshift/support/conversations/usersetup/UserSetupFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_9

    const-class v3, Lcom/helpshift/support/conversations/AuthenticationFailureFragment;

    .line 361
    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 363
    :cond_9
    invoke-direct {p0, v4}, Lcom/helpshift/support/fragments/SupportFragment;->setRetainSearchFragmentState(Z)V

    const/4 v2, 0x0

    .line 364
    invoke-virtual {p0, v2}, Lcom/helpshift/support/fragments/SupportFragment;->setSearchMenuVisible(Z)V

    .line 365
    invoke-virtual {p0, v2}, Lcom/helpshift/support/fragments/SupportFragment;->setContactUsMenuVisible(Z)V

    goto/16 :goto_0

    .line 352
    :cond_a
    :goto_1
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->restoreConversationFragmentMenu()V

    goto/16 :goto_0

    .line 368
    :cond_b
    monitor-exit v0

    goto :goto_2

    :catchall_0
    move-exception v1

    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v1

    :cond_c
    :goto_2
    return-void
.end method

.method public registerToolbarMenuEventsListener(Lcom/helpshift/support/fragments/IMenuItemEventListener;)V
    .locals 1

    .line 716
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method public removeVisibleFragment(Ljava/lang/String;)V
    .locals 1

    .line 317
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->remove(Ljava/lang/Object;)Z

    return-void
.end method

.method public resetNewMessageCount()V
    .locals 1

    const/4 v0, 0x0

    .line 491
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/helpshift/support/fragments/SupportFragment;->updateMessageBatchCount(Ljava/lang/Integer;)V

    return-void
.end method

.method public resetToolbarImportanceForAccessibility()V
    .locals 2

    .line 1035
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-ge v0, v1, :cond_0

    return-void

    .line 1039
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-eqz v0, :cond_1

    .line 1040
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    iget v1, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarImportanceForAccessibility:I

    invoke-virtual {v0, v1}, Landroidx/appcompat/widget/Toolbar;->setImportantForAccessibility(I)V

    goto :goto_0

    .line 1043
    :cond_1
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    .line 1044
    instance-of v1, v0, Lcom/helpshift/support/activities/ParentActivity;

    if-eqz v1, :cond_2

    .line 1045
    check-cast v0, Lcom/helpshift/support/activities/ParentActivity;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/support/activities/ParentActivity;->setToolbarImportanceForAccessibility(I)V

    :cond_2
    :goto_0
    return-void
.end method

.method public setContactUsMenuVisible(Z)V
    .locals 1

    .line 444
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->isActionViewExpanded(Landroid/view/MenuItem;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 445
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    const/4 v0, 0x0

    invoke-interface {p1, v0}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    goto :goto_0

    .line 448
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->contactUsMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, p1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    .line 450
    :goto_0
    invoke-direct {p0}, Lcom/helpshift/support/fragments/SupportFragment;->updateBadgeIcon()V

    return-void
.end method

.method public setSearchListeners(Lcom/helpshift/support/controllers/FaqFlowController;)V
    .locals 1

    .line 275
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemsPrepared:Z

    if-eqz v0, :cond_1

    if-nez p1, :cond_0

    .line 277
    invoke-virtual {p0}, Lcom/helpshift/support/fragments/SupportFragment;->getRetainedChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-static {v0}, Lcom/helpshift/support/util/FragmentUtil;->getFaqFlowFragment(Landroidx/fragment/app/FragmentManager;)Lcom/helpshift/support/fragments/FaqFlowFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 279
    invoke-virtual {v0}, Lcom/helpshift/support/fragments/FaqFlowFragment;->getFaqFlowController()Lcom/helpshift/support/controllers/FaqFlowController;

    move-result-object p1

    :cond_0
    if-eqz p1, :cond_1

    .line 284
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0, p1}, Lcom/helpshift/views/HSMenuItemCompat;->setOnActionExpandListener(Landroid/view/MenuItem;Landroid/view/MenuItem$OnActionExpandListener;)V

    .line 285
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchView:Landroidx/appcompat/widget/SearchView;

    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/SearchView;->setOnQueryTextListener(Landroidx/appcompat/widget/SearchView$OnQueryTextListener;)V

    :cond_1
    return-void
.end method

.method public setSearchMenuQuery(Ljava/lang/String;)V
    .locals 2

    .line 462
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->isActionViewExpanded(Landroid/view/MenuItem;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 463
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->expandActionView(Landroid/view/MenuItem;)V

    .line 466
    :cond_0
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 467
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchView:Landroidx/appcompat/widget/SearchView;

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroidx/appcompat/widget/SearchView;->setQuery(Ljava/lang/CharSequence;Z)V

    :cond_1
    return-void
.end method

.method public setSearchMenuVisible(Z)V
    .locals 2

    .line 454
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->isActionViewExpanded(Landroid/view/MenuItem;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->visibleFragments:Ljava/util/List;

    const-class v1, Lcom/helpshift/support/fragments/SearchFragment;

    .line 455
    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 456
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-static {v0}, Lcom/helpshift/views/HSMenuItemCompat;->collapseActionView(Landroid/view/MenuItem;)V

    .line 458
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    invoke-interface {v0, p1}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    return-void
.end method

.method public setTitle(Ljava/lang/String;)V
    .locals 1

    .line 606
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-eqz v0, :cond_0

    .line 607
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/Toolbar;->setTitle(Ljava/lang/CharSequence;)V

    goto :goto_0

    .line 610
    :cond_0
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/app/AppCompatActivity;

    invoke-virtual {v0}, Landroidx/appcompat/app/AppCompatActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 612
    invoke-virtual {v0, p1}, Landroidx/appcompat/app/ActionBar;->setTitle(Ljava/lang/CharSequence;)V

    :cond_1
    :goto_0
    return-void
.end method

.method public setToolbarImportanceForAccessibility(I)V
    .locals 2

    .line 1016
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-ge v0, v1, :cond_0

    return-void

    .line 1020
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-eqz v0, :cond_1

    .line 1022
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0}, Landroidx/appcompat/widget/Toolbar;->getImportantForAccessibility()I

    move-result v0

    iput v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbarImportanceForAccessibility:I

    .line 1023
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/Toolbar;->setImportantForAccessibility(I)V

    goto :goto_0

    .line 1026
    :cond_1
    invoke-virtual {p0, p0}, Lcom/helpshift/support/fragments/SupportFragment;->getActivity(Landroidx/fragment/app/Fragment;)Landroid/app/Activity;

    move-result-object v0

    .line 1027
    instance-of v1, v0, Lcom/helpshift/support/activities/ParentActivity;

    if-eqz v1, :cond_2

    .line 1028
    check-cast v0, Lcom/helpshift/support/activities/ParentActivity;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/activities/ParentActivity;->setToolbarImportanceForAccessibility(I)V

    :cond_2
    :goto_0
    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x0

    return v0
.end method

.method public showToolbarElevation(Z)V
    .locals 2

    .line 618
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    .line 619
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->showToolbarElevationLollipop(Z)V

    goto :goto_0

    .line 622
    :cond_0
    invoke-direct {p0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->showToolbarElevationPreLollipop(Z)V

    :goto_0
    return-void
.end method

.method public unRegisterSearchListener()V
    .locals 2

    .line 268
    iget-boolean v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemsPrepared:Z

    if-eqz v0, :cond_0

    .line 269
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchMenuItem:Landroid/view/MenuItem;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lcom/helpshift/views/HSMenuItemCompat;->setOnActionExpandListener(Landroid/view/MenuItem;Landroid/view/MenuItem$OnActionExpandListener;)V

    .line 270
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->searchView:Landroidx/appcompat/widget/SearchView;

    invoke-virtual {v0, v1}, Landroidx/appcompat/widget/SearchView;->setOnQueryTextListener(Landroidx/appcompat/widget/SearchView$OnQueryTextListener;)V

    :cond_0
    return-void
.end method

.method public unRegisterToolbarMenuEventsListener(Lcom/helpshift/support/fragments/IMenuItemEventListener;)V
    .locals 1

    .line 720
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    .line 721
    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-ne v0, p1, :cond_0

    const/4 p1, 0x0

    .line 722
    iput-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->menuItemEventListener:Ljava/lang/ref/WeakReference;

    :cond_0
    return-void
.end method

.method public updateFaqLoadingUI(I)V
    .locals 2

    .line 495
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewNoFaqs:Landroid/view/View;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 496
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoading:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    .line 497
    iget-object v0, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoadError:Landroid/view/View;

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    const/4 v0, 0x0

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 509
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoadError:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 506
    :pswitch_1
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewNoFaqs:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 501
    :pswitch_2
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->viewFaqsLoading:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    :pswitch_3
    return-void

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_2
        :pswitch_3
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public updateMenuItemVisibility(Lcom/helpshift/support/fragments/HSMenuItemType;Z)V
    .locals 1

    .line 745
    sget-object v0, Lcom/helpshift/support/fragments/SupportFragment$2;->$SwitchMap$com$helpshift$support$fragments$HSMenuItemType:[I

    invoke-virtual {p1}, Lcom/helpshift/support/fragments/HSMenuItemType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    .line 752
    :pswitch_0
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    if-eqz p1, :cond_0

    .line 753
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->attachImageMenuItem:Landroid/view/MenuItem;

    invoke-interface {p1, p2}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    goto :goto_0

    .line 747
    :pswitch_1
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    if-eqz p1, :cond_0

    .line 748
    iget-object p1, p0, Lcom/helpshift/support/fragments/SupportFragment;->startNewConversationMenuItem:Landroid/view/MenuItem;

    invoke-interface {p1, p2}, Landroid/view/MenuItem;->setVisible(Z)Landroid/view/MenuItem;

    :cond_0
    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
