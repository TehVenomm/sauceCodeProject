.class public Lcom/helpshift/support/compositions/SectionPagerAdapter;
.super Landroidx/fragment/app/FragmentStatePagerAdapter;
.source "SectionPagerAdapter.java"


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_SectionPager"


# instance fields
.field private faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

.field private sections:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/support/Section;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroidx/fragment/app/FragmentManager;Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroidx/fragment/app/FragmentManager;",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Section;",
            ">;",
            "Lcom/helpshift/support/FaqTagFilter;",
            ")V"
        }
    .end annotation

    .line 24
    invoke-direct {p0, p1}, Landroidx/fragment/app/FragmentStatePagerAdapter;-><init>(Landroidx/fragment/app/FragmentManager;)V

    .line 25
    iput-object p2, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->sections:Ljava/util/List;

    .line 26
    iput-object p3, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    return-void
.end method


# virtual methods
.method public getCount()I
    .locals 1

    .line 56
    iget-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->sections:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    return v0
.end method

.method public getItem(I)Landroidx/fragment/app/Fragment;
    .locals 3

    .line 31
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "sectionPublishId"

    .line 32
    iget-object v2, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->sections:Ljava/util/List;

    invoke-interface {v2, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/Section;

    invoke-virtual {p1}, Lcom/helpshift/support/Section;->getPublishId()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "withTagsMatching"

    .line 33
    iget-object v1, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->faqTagFilter:Lcom/helpshift/support/FaqTagFilter;

    invoke-virtual {v0, p1, v1}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    .line 34
    invoke-static {v0}, Lcom/helpshift/support/fragments/QuestionListFragment;->newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/fragments/QuestionListFragment;

    move-result-object p1

    return-object p1
.end method

.method public getPageTitle(I)Ljava/lang/CharSequence;
    .locals 1

    .line 61
    iget-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerAdapter;->sections:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/helpshift/support/Section;

    invoke-virtual {p1}, Lcom/helpshift/support/Section;->getTitle()Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public restoreState(Landroid/os/Parcelable;Ljava/lang/ClassLoader;)V
    .locals 1

    .line 47
    :try_start_0
    invoke-super {p0, p1, p2}, Landroidx/fragment/app/FragmentStatePagerAdapter;->restoreState(Landroid/os/Parcelable;Ljava/lang/ClassLoader;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_SectionPager"

    const-string v0, "Exception in restoreState: "

    .line 50
    invoke-static {p2, v0, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    :goto_0
    return-void
.end method
