.class public Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;
.super Landroidx/appcompat/app/AppCompatActivity;


# static fields
.field public static final EXTRA_COMMENT:Ljava/lang/String; = "COMMENT"

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimCommentActivity"


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroidx/appcompat/app/AppCompatActivity;-><init>()V

    return-void
.end method


# virtual methods
.method protected onCreate(Landroid/os/Bundle;)V
    .locals 3

    invoke-super {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;->onCreate(Landroid/os/Bundle;)V

    sget p1, Lcom/zopim/android/sdk/R$layout;->zopim_comment_activity:I

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->setContentView(I)V

    sget p1, Lcom/zopim/android/sdk/R$id;->toolbar:I

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->setSupportActionBar(Landroidx/appcompat/widget/Toolbar;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object p1

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Landroidx/appcompat/app/ActionBar;->setDisplayHomeAsUpEnabled(Z)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    const-class v0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-nez v0, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    const-string v1, "COMMENT"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;->newInstance(Ljava/lang/String;)Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;

    move-result-object v0

    goto :goto_1

    :cond_1
    new-instance v0, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;-><init>()V

    :goto_1
    invoke-virtual {p1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p1

    sget v1, Lcom/zopim/android/sdk/R$id;->comment_fragment_container:I

    const-class v2, Lcom/zopim/android/sdk/chatlog/ZopimCommentFragment;

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v1, v0, v2}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_2
    return-void
.end method

.method protected onDestroy()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Activity destroyed"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-super {p0}, Landroidx/appcompat/app/AppCompatActivity;->onDestroy()V

    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->finish()V

    invoke-super {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    :cond_0
    sget v0, Lcom/zopim/android/sdk/R$id;->send_comment:I

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result p1

    const/4 v1, 0x0

    if-ne v0, p1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->finish()V

    :cond_1
    return v1
.end method

.method protected onStop()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    invoke-super {p0}, Landroidx/appcompat/app/AppCompatActivity;->onStop()V

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->isFinishing()Z

    move-result v0

    :goto_0
    if-eqz v0, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;->finish()V

    :cond_2
    return-void
.end method
