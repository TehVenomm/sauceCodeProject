.class public Lcom/helpshift/support/compositions/SectionPagerFragment;
.super Lcom/helpshift/support/fragments/MainFragment;
.source "SectionPagerFragment.java"

# interfaces
.implements Lcom/helpshift/support/contracts/FaqFlowViewParent;


# instance fields
.field private tabLayout:Lcom/google/android/material/tabs/TabLayout;

.field private tabLayoutPadding:I

.field private viewPagerContainer:Landroid/widget/FrameLayout;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 25
    invoke-direct {p0}, Lcom/helpshift/support/fragments/MainFragment;-><init>()V

    const/4 v0, 0x0

    .line 29
    iput v0, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayoutPadding:I

    return-void
.end method

.method private getSectionPosition(Ljava/util/List;Ljava/lang/String;)I
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/support/Section;",
            ">;",
            "Ljava/lang/String;",
            ")I"
        }
    .end annotation

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 91
    :goto_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v2

    if-ge v1, v2, :cond_1

    .line 92
    invoke-interface {p1, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/support/Section;

    .line 93
    invoke-virtual {v2}, Lcom/helpshift/support/Section;->getPublishId()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    return v1

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    return v0
.end method

.method public static newInstance(Landroid/os/Bundle;)Lcom/helpshift/support/compositions/SectionPagerFragment;
    .locals 1

    .line 32
    new-instance v0, Lcom/helpshift/support/compositions/SectionPagerFragment;

    invoke-direct {v0}, Lcom/helpshift/support/compositions/SectionPagerFragment;-><init>()V

    .line 33
    invoke-virtual {v0, p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private showTabLayoutElevation()V
    .locals 3

    .line 108
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_0

    .line 109
    iget-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayout:Lcom/google/android/material/tabs/TabLayout;

    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getContext()Landroid/content/Context;

    move-result-object v1

    const/high16 v2, 0x40800000    # 4.0f

    invoke-static {v1, v2}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result v1

    invoke-virtual {v0, v1}, Lcom/google/android/material/tabs/TabLayout;->setElevation(F)V

    goto :goto_0

    .line 113
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->viewPagerContainer:Landroid/widget/FrameLayout;

    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    sget v2, Lcom/helpshift/R$drawable;->hs__actionbar_compat_shadow:I

    invoke-virtual {v1, v2}, Landroid/content/res/Resources;->getDrawable(I)Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->setForeground(Landroid/graphics/drawable/Drawable;)V

    :goto_0
    return-void
.end method

.method private showToolbarElevation(Z)V
    .locals 1

    .line 101
    invoke-static {p0}, Lcom/helpshift/support/util/FragmentUtil;->getSupportFragment(Landroidx/fragment/app/Fragment;)Lcom/helpshift/support/fragments/SupportFragment;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 103
    invoke-virtual {v0, p1}, Lcom/helpshift/support/fragments/SupportFragment;->showToolbarElevation(Z)V

    :cond_0
    return-void
.end method


# virtual methods
.method public getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;
    .locals 1

    .line 39
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/contracts/FaqFlowViewParent;

    invoke-interface {v0}, Lcom/helpshift/support/contracts/FaqFlowViewParent;->getFaqFlowListener()Lcom/helpshift/support/contracts/FaqFragmentListener;

    move-result-object v0

    return-object v0
.end method

.method public onAttach(Landroid/content/Context;)V
    .locals 1

    .line 75
    invoke-super {p0, p1}, Lcom/helpshift/support/fragments/MainFragment;->onAttach(Landroid/content/Context;)V

    const/high16 v0, 0x42400000    # 48.0f

    .line 76
    invoke-static {p1, v0}, Lcom/helpshift/util/Styles;->dpToPx(Landroid/content/Context;F)F

    move-result p1

    float-to-int p1, p1

    iput p1, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayoutPadding:I

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 45
    sget p3, Lcom/helpshift/R$layout;->hs__section_pager_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDestroyView()V
    .locals 1

    const/4 v0, 0x0

    .line 120
    iput-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayout:Lcom/google/android/material/tabs/TabLayout;

    .line 121
    iput-object v0, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->viewPagerContainer:Landroid/widget/FrameLayout;

    .line 122
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onDestroyView()V

    return-void
.end method

.method public onResume()V
    .locals 1

    .line 68
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onResume()V

    const/4 v0, 0x0

    .line 69
    invoke-direct {p0, v0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->showToolbarElevation(Z)V

    .line 70
    invoke-direct {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->showTabLayoutElevation()V

    return-void
.end method

.method public onStop()V
    .locals 1

    const/4 v0, 0x1

    .line 81
    invoke-direct {p0, v0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->showToolbarElevation(Z)V

    .line 82
    invoke-super {p0}, Lcom/helpshift/support/fragments/MainFragment;->onStop()V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 5

    .line 50
    invoke-super {p0, p1, p2}, Lcom/helpshift/support/fragments/MainFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    .line 52
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p2

    const-string v0, "sections"

    invoke-virtual {p2, v0}, Landroid/os/Bundle;->getParcelableArrayList(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p2

    .line 53
    sget v0, Lcom/helpshift/R$id;->section_pager:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/viewpager/widget/ViewPager;

    .line 54
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "withTagsMatching"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v1

    check-cast v1, Lcom/helpshift/support/FaqTagFilter;

    .line 55
    new-instance v2, Lcom/helpshift/support/compositions/SectionPagerAdapter;

    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v3

    invoke-direct {v2, v3, p2, v1}, Lcom/helpshift/support/compositions/SectionPagerAdapter;-><init>(Landroidx/fragment/app/FragmentManager;Ljava/util/List;Lcom/helpshift/support/FaqTagFilter;)V

    invoke-virtual {v0, v2}, Landroidx/viewpager/widget/ViewPager;->setAdapter(Landroidx/viewpager/widget/PagerAdapter;)V

    .line 58
    sget v1, Lcom/helpshift/R$id;->pager_tabs:I

    invoke-virtual {p1, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Lcom/google/android/material/tabs/TabLayout;

    iput-object v1, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayout:Lcom/google/android/material/tabs/TabLayout;

    .line 59
    iget-object v1, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayout:Lcom/google/android/material/tabs/TabLayout;

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Lcom/google/android/material/tabs/TabLayout;->getChildAt(I)Landroid/view/View;

    move-result-object v1

    iget v3, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayoutPadding:I

    iget v4, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayoutPadding:I

    invoke-virtual {v1, v3, v2, v4, v2}, Landroid/view/View;->setPadding(IIII)V

    .line 60
    iget-object v1, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->tabLayout:Lcom/google/android/material/tabs/TabLayout;

    invoke-virtual {v1, v0}, Lcom/google/android/material/tabs/TabLayout;->setupWithViewPager(Landroidx/viewpager/widget/ViewPager;)V

    .line 61
    invoke-virtual {p0}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "sectionPublishId"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 62
    invoke-direct {p0, p2, v1}, Lcom/helpshift/support/compositions/SectionPagerFragment;->getSectionPosition(Ljava/util/List;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {v0, p2}, Landroidx/viewpager/widget/ViewPager;->setCurrentItem(I)V

    .line 63
    sget p2, Lcom/helpshift/R$id;->view_pager_container:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/FrameLayout;

    iput-object p1, p0, Lcom/helpshift/support/compositions/SectionPagerFragment;->viewPagerContainer:Landroid/widget/FrameLayout;

    return-void
.end method

.method public shouldRefreshMenu()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method
