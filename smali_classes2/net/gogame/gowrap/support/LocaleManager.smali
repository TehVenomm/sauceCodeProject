.class public Lnet/gogame/gowrap/support/LocaleManager;
.super Ljava/lang/Object;
.source "LocaleManager.java"


# instance fields
.field private final context:Landroid/content/Context;

.field private final supportedLocaleDescriptors:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/support/LocaleDescriptor;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 6

    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 29
    iput-object p1, p0, Lnet/gogame/gowrap/support/LocaleManager;->context:Landroid/content/Context;

    .line 31
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    sget v1, Lnet/gogame/gowrap/R$array;->language_values:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->getStringArray(I)[Ljava/lang/String;

    move-result-object v0

    .line 32
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v1, Lnet/gogame/gowrap/R$array;->languages:I

    invoke-virtual {p1, v1}, Landroid/content/res/Resources;->getStringArray(I)[Ljava/lang/String;

    move-result-object p1

    .line 33
    new-instance v1, Ljava/util/LinkedHashMap;

    invoke-direct {v1}, Ljava/util/LinkedHashMap;-><init>()V

    const/4 v2, 0x0

    .line 34
    :goto_0
    array-length v3, v0

    if-ge v2, v3, :cond_1

    .line 35
    aget-object v3, v0, v2

    const/4 v4, 0x0

    .line 37
    array-length v5, p1

    if-ge v2, v5, :cond_0

    .line 38
    aget-object v4, p1, v2

    .line 40
    :cond_0
    new-instance v5, Lnet/gogame/gowrap/support/LocaleDescriptor;

    invoke-direct {v5, v3, v4}, Lnet/gogame/gowrap/support/LocaleDescriptor;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v1, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 43
    :cond_1
    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->getSupportedLocales()Ljava/util/List;

    move-result-object p1

    if-nez p1, :cond_2

    .line 45
    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    .line 47
    :cond_2
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    if-eqz p1, :cond_4

    .line 49
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_3
    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 50
    invoke-interface {v1, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/support/LocaleDescriptor;

    if-eqz v0, :cond_3

    .line 52
    iget-object v2, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    invoke-interface {v2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_1

    :cond_4
    return-void
.end method


# virtual methods
.method public getSupportedLocaleDescriptorById(Ljava/lang/String;)Lnet/gogame/gowrap/support/LocaleDescriptor;
    .locals 3

    .line 63
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    if-eqz v0, :cond_1

    if-eqz p1, :cond_1

    .line 64
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/LocaleDescriptor;

    if-eqz v1, :cond_0

    .line 66
    invoke-virtual {v1}, Lnet/gogame/gowrap/support/LocaleDescriptor;->getId()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2, p1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    return-object v1

    :cond_1
    const/4 p1, 0x0

    return-object p1
.end method

.method public getSupportedLocaleDescriptorByIndex(I)Lnet/gogame/gowrap/support/LocaleDescriptor;
    .locals 1

    .line 76
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    if-eqz v0, :cond_0

    if-ltz p1, :cond_0

    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    .line 77
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    if-ge p1, v0, :cond_0

    .line 78
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    invoke-interface {v0, p1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lnet/gogame/gowrap/support/LocaleDescriptor;

    return-object p1

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getSupportedLocaleDescriptorIndex(Ljava/lang/String;)I
    .locals 2

    if-eqz p1, :cond_1

    .line 84
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    if-eqz v0, :cond_1

    const/4 v0, 0x0

    .line 85
    :goto_0
    iget-object v1, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->size()I

    move-result v1

    if-ge v0, v1, :cond_1

    .line 86
    iget-object v1, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    invoke-interface {v1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/LocaleDescriptor;

    if-eqz v1, :cond_0

    .line 88
    invoke-virtual {v1}, Lnet/gogame/gowrap/support/LocaleDescriptor;->getId()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Lnet/gogame/gowrap/support/StringUtils;->isEquals(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    return v0

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 p1, -0x1

    return p1
.end method

.method public getSupportedLocaleDescriptors()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/support/LocaleDescriptor;",
            ">;"
        }
    .end annotation

    .line 59
    iget-object v0, p0, Lnet/gogame/gowrap/support/LocaleManager;->supportedLocaleDescriptors:Ljava/util/List;

    return-object v0
.end method

.method public setLocale(Ljava/lang/String;)V
    .locals 7

    .line 98
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v1, p0, Lnet/gogame/gowrap/support/LocaleManager;->context:Landroid/content/Context;

    invoke-virtual {v0, v1, p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->setCurrentLocale(Landroid/content/Context;Ljava/lang/String;)V

    .line 99
    sget-object v0, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    iget-object v1, p0, Lnet/gogame/gowrap/support/LocaleManager;->context:Landroid/content/Context;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->readConfiguration(Landroid/content/Context;)V

    const-string v0, "default"

    .line 102
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x2

    const/4 v2, 0x1

    const/4 v3, 0x0

    if-eqz v0, :cond_0

    .line 103
    sget-object v0, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    :goto_0
    move-object v4, v0

    goto :goto_2

    .line 105
    :cond_0
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v4, 0x0

    :goto_1
    const/16 v5, 0x5f

    .line 108
    invoke-virtual {p1, v5, v4}, Ljava/lang/String;->indexOf(II)I

    move-result v5

    const/4 v6, -0x1

    if-ne v5, v6, :cond_4

    .line 110
    invoke-virtual {p1, v4}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v4

    .line 111
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 119
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v4

    const/4 v5, 0x3

    if-lt v4, v5, :cond_1

    .line 120
    new-instance v4, Ljava/util/Locale;

    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    invoke-direct {v4, v5, v6, v0}, Ljava/util/Locale;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_2

    .line 121
    :cond_1
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v4

    if-ne v4, v1, :cond_2

    .line 122
    new-instance v4, Ljava/util/Locale;

    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    invoke-direct {v4, v5, v0}, Ljava/util/Locale;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_2

    .line 123
    :cond_2
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v4

    if-ne v4, v2, :cond_3

    .line 124
    new-instance v4, Ljava/util/Locale;

    invoke-interface {v0, v3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    invoke-direct {v4, v0}, Ljava/util/Locale;-><init>(Ljava/lang/String;)V

    goto :goto_2

    .line 126
    :cond_3
    sget-object v0, Ljava/util/Locale;->ENGLISH:Ljava/util/Locale;

    goto :goto_0

    :goto_2
    const-string v0, "goWrap"

    const-string v5, "Locale set to %s / %s"

    .line 130
    new-array v1, v1, [Ljava/lang/Object;

    aput-object p1, v1, v3

    aput-object v4, v1, v2

    invoke-static {v5, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    .line 131
    invoke-static {v4}, Ljava/util/Locale;->setDefault(Ljava/util/Locale;)V

    .line 132
    iget-object p1, p0, Lnet/gogame/gowrap/support/LocaleManager;->context:Landroid/content/Context;

    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    .line 133
    invoke-virtual {p1}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    .line 134
    invoke-virtual {p1}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v1

    .line 135
    iput-object v4, v1, Landroid/content/res/Configuration;->locale:Ljava/util/Locale;

    .line 136
    invoke-virtual {p1, v1, v0}, Landroid/content/res/Resources;->updateConfiguration(Landroid/content/res/Configuration;Landroid/util/DisplayMetrics;)V

    return-void

    .line 114
    :cond_4
    invoke-virtual {p1, v4, v5}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v4

    .line 115
    invoke-interface {v0, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v4, v5, 0x1

    goto/16 :goto_1
.end method
