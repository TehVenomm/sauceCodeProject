.class public Lcom/helpshift/conversation/activeconversation/message/input/TextInput;
.super Lcom/helpshift/conversation/activeconversation/message/input/Input;
.source "TextInput.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/activeconversation/message/input/TextInput$Keyboard;
    }
.end annotation


# instance fields
.field private domain:Lcom/helpshift/common/domain/Domain;

.field public final keyboard:I

.field public final placeholder:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 0

    .line 21
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/helpshift/conversation/activeconversation/message/input/Input;-><init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;)V

    .line 22
    iput-object p5, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    .line 23
    iput p6, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 4

    .line 65
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 69
    :cond_0
    move-object v0, p1

    check-cast v0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;

    .line 71
    iget v2, v0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    iget v3, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    if-ne v2, v3, :cond_1

    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->placeholder:Ljava/lang/String;

    .line 72
    invoke-static {v0, v2}, Lcom/helpshift/common/StringUtils;->isEqual(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 73
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/input/Input;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method

.method public setDependencies(Lcom/helpshift/common/domain/Domain;)V
    .locals 0

    .line 27
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->domain:Lcom/helpshift/common/domain/Domain;

    return-void
.end method

.method public validate(Ljava/lang/String;)Z
    .locals 3

    .line 38
    iget v0, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->keyboard:I

    const/4 v1, 0x1

    packed-switch v0, :pswitch_data_0

    return v1

    .line 48
    :pswitch_0
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/input/TextInput;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/Domain;->getLocaleProviderDM()Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;

    move-result-object v0

    invoke-virtual {v0}, Lcom/helpshift/localeprovider/domainmodel/LocaleProviderDM;->getCurrentLocale()Ljava/util/Locale;

    move-result-object v0

    const-string v2, "EEEE, MMMM dd, yyyy"

    .line 50
    invoke-static {v2, v0}, Lcom/helpshift/common/util/HSDateFormatSpec;->getDateFormatter(Ljava/lang/String;Ljava/util/Locale;)Lcom/helpshift/common/util/HSSimpleDateFormat;

    move-result-object v0

    .line 51
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/helpshift/common/util/HSSimpleDateFormat;->parse(Ljava/lang/String;)Ljava/util/Date;
    :try_end_0
    .catch Ljava/text/ParseException; {:try_start_0 .. :try_end_0} :catch_0

    return v1

    :catch_0
    const/4 p1, 0x0

    return p1

    .line 42
    :pswitch_1
    invoke-static {p1}, Lcom/helpshift/util/HSPattern;->isPositiveNumber(Ljava/lang/String;)Z

    move-result p1

    return p1

    .line 40
    :pswitch_2
    invoke-static {p1}, Lcom/helpshift/util/HSPattern;->isValidEmail(Ljava/lang/String;)Z

    move-result p1

    return p1

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
