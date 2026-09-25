.class public Lcom/helpshift/support/activities/ParentActivity;
.super Lcom/helpshift/activities/MainActivity;
.source "ParentActivity.java"


# instance fields
.field fragmentManager:Landroidx/fragment/app/FragmentManager;

.field private toolbar:Landroidx/appcompat/widget/Toolbar;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 21
    invoke-direct {p0}, Lcom/helpshift/activities/MainActivity;-><init>()V

    return-void
.end method


# virtual methods
.method public onBackPressed()V
    .locals 3

    .line 63
    iget-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_2

    .line 65
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/fragment/app/Fragment;

    if-eqz v1, :cond_0

    .line 67
    invoke-virtual {v1}, Landroidx/fragment/app/Fragment;->isVisible()Z

    move-result v2

    if-eqz v2, :cond_0

    instance-of v2, v1, Lcom/helpshift/support/fragments/SupportFragment;

    if-eqz v2, :cond_0

    .line 69
    move-object v2, v1

    check-cast v2, Lcom/helpshift/support/fragments/SupportFragment;

    invoke-virtual {v2}, Lcom/helpshift/support/fragments/SupportFragment;->onBackPressed()Z

    move-result v2

    if-eqz v2, :cond_1

    return-void

    .line 73
    :cond_1
    invoke-virtual {v1}, Landroidx/fragment/app/Fragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    .line 74
    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->getBackStackEntryCount()I

    move-result v2

    if-lez v2, :cond_0

    .line 75
    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->popBackStack()V

    return-void

    .line 82
    :cond_2
    invoke-super {p0}, Lcom/helpshift/activities/MainActivity;->onBackPressed()V

    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 2

    .line 28
    invoke-super {p0, p1}, Lcom/helpshift/activities/MainActivity;->onCreate(Landroid/os/Bundle;)V

    .line 31
    sget-object v0, Lcom/helpshift/util/HelpshiftContext;->installCallSuccessful:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_1

    .line 33
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->getPackageName()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/util/ApplicationUtil;->getLaunchIntent(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 36
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->finish()V

    .line 37
    invoke-virtual {p0, p1}, Lcom/helpshift/support/activities/ParentActivity;->startActivity(Landroid/content/Intent;)V

    :cond_0
    return-void

    .line 42
    :cond_1
    sget v0, Lcom/helpshift/R$layout;->hs__parent_activity:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/activities/ParentActivity;->setContentView(I)V

    .line 44
    sget v0, Lcom/helpshift/R$id;->toolbar:I

    invoke-virtual {p0, v0}, Lcom/helpshift/support/activities/ParentActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/Toolbar;

    iput-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->toolbar:Landroidx/appcompat/widget/Toolbar;

    .line 45
    iget-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p0, v0}, Lcom/helpshift/support/activities/ParentActivity;->setSupportActionBar(Landroidx/appcompat/widget/Toolbar;)V

    .line 46
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    if-eqz v0, :cond_2

    const/4 v1, 0x1

    .line 48
    invoke-virtual {v0, v1}, Landroidx/appcompat/app/ActionBar;->setDisplayHomeAsUpEnabled(Z)V

    .line 51
    :cond_2
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    if-nez p1, :cond_3

    .line 54
    iget-object p1, p0, Lcom/helpshift/support/activities/ParentActivity;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p1

    .line 55
    sget v0, Lcom/helpshift/R$id;->support_fragment_container:I

    .line 56
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v1

    invoke-static {v1}, Lcom/helpshift/support/fragments/SupportFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v1

    .line 55
    invoke-virtual {p1, v0, v1}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    .line 57
    invoke-virtual {p1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_3
    return-void
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 3

    .line 87
    invoke-super {p0, p1}, Lcom/helpshift/activities/MainActivity;->onNewIntent(Landroid/content/Intent;)V

    .line 88
    iget-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->fragmentManager:Landroidx/fragment/app/FragmentManager;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->getFragments()Ljava/util/List;

    move-result-object v0

    if-nez v0, :cond_0

    return-void

    .line 93
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroidx/fragment/app/Fragment;

    .line 94
    instance-of v2, v1, Lcom/helpshift/support/fragments/SupportFragment;

    if-eqz v2, :cond_1

    .line 95
    check-cast v1, Lcom/helpshift/support/fragments/SupportFragment;

    invoke-virtual {p1}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/helpshift/support/fragments/SupportFragment;->onNewIntent(Landroid/os/Bundle;)V

    goto :goto_0

    :cond_2
    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    .line 102
    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-eq v0, v1, :cond_0

    .line 109
    invoke-super {p0, p1}, Lcom/helpshift/activities/MainActivity;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    .line 104
    :cond_0
    invoke-virtual {p0}, Lcom/helpshift/support/activities/ParentActivity;->onBackPressed()V

    const/4 p1, 0x1

    return p1
.end method

.method public setToolbarImportanceForAccessibility(I)V
    .locals 2

    .line 117
    iget-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->toolbar:Landroidx/appcompat/widget/Toolbar;

    if-eqz v0, :cond_0

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-lt v0, v1, :cond_0

    .line 118
    iget-object v0, p0, Lcom/helpshift/support/activities/ParentActivity;->toolbar:Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {v0, p1}, Landroidx/appcompat/widget/Toolbar;->setImportantForAccessibility(I)V

    :cond_0
    return-void
.end method
